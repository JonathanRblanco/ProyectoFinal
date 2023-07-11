using Ardalis.Result;
using AutoMapper;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.UserHanlders
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, Result<IEnumerable<GetUsersResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetUsersHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<GetUsersResponse>>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await unitOfWork.UsersRepository.GetAllUsersAsync(unitOfWork.Transaction);
                var response = mapper.Map<IEnumerable<GetUsersResponse>>(users);
                return Result.Success(response);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
