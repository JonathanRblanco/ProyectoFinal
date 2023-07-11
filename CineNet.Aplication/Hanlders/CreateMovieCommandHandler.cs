using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, CreateMovieCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateMovieCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CreateMovieCommandResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            try
            {
                unitOfWork.BeginTransaction();
                using (var binaryReader = new BinaryReader(request.Image.OpenReadStream()))
                {
                    var imageData = binaryReader.ReadBytes((int)request.Image.Length);
                    var imageId = await unitOfWork.ImagesRepository.Create(imageData, unitOfWork.Transaction);
                    var movie = mapper.Map<Movie>(request);
                    movie.ImageId = imageId;
                    movie.Id = await unitOfWork.MoviesRepository.CreateMovie(movie, unitOfWork.Transaction);
                    unitOfWork.CommitTransaction();
                    return mapper.Map<CreateMovieCommandResponse>(movie);
                }
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                throw;
            }
        }
    }
}
