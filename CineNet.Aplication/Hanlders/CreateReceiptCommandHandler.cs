using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;
using System.Text.Json;

namespace CineNet.Aplication.Hanlders
{
    public class CreateReceiptCommandHandler : IRequestHandler<CreateReceiptCommand, CreateReceiptCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateReceiptCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CreateReceiptCommandResponse> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
        {
            try
            {
                unitOfWork.BeginTransaction();
                var show = await unitOfWork.ShowsRepository.GetById(request.ShowId, unitOfWork.Transaction);
                show.Capacity -= request.AmountOfTickets;
                await unitOfWork.ShowsRepository.Update(show, unitOfWork.Transaction);
                var receipt = mapper.Map<Receipt>(request);
                receipt.Date = DateTime.UtcNow;
                receipt.Code = GenerateRandomCode(new Random(), 7);
                receipt.Id = await unitOfWork.ReceiptsRepository.Create(receipt, unitOfWork.Transaction);
                await unitOfWork.EmailTasksRepository.Create(new EmailTask { Data = JsonSerializer.Serialize(new { ReceiptId = receipt.Id, UserId = request.UserId }), Status = "Pendiente", Type = "SendReceipt" }, unitOfWork.Transaction);
                unitOfWork.CommitTransaction();
                receipt.Show = await unitOfWork.ShowsRepository.GetById(receipt.ShowId, unitOfWork.Transaction);
                receipt.Show.Movie = await unitOfWork.MoviesRepository.GetMovieByIdAsync(receipt.Show.MovieId, unitOfWork.Transaction);
                receipt.Show.ShowType = await unitOfWork.ShowsTypeRepository.GetById(receipt.Show.ShowTypeId, unitOfWork.Transaction);
                receipt.Show.Room = await unitOfWork.RoomsRepository.GetById(receipt.Show.RoomId, unitOfWork.Transaction);
                receipt.Show.Room.CinemaBranch = await unitOfWork.BranchesRepository.GetById(receipt.Show.Room.CinemaBranchId, unitOfWork.Transaction);
                return mapper.Map<CreateReceiptCommandResponse>(receipt);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                throw;
            }
        }
        private string GenerateRandomCode(Random random, int length)
        {
            const string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] codigo = new char[length];

            for (int i = 0; i < length; i++)
            {
                codigo[i] = caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
            }

            return FormatCode(new string(codigo));
        }

        private string FormatCode(string codigo)
        {
            string primeraMitad = codigo.Substring(0, codigo.Length / 2);
            string segundaMitad = codigo.Substring(codigo.Length / 2);

            return $"{primeraMitad}-{segundaMitad}";
        }
    }
}
