using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, UpdateMovieCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateMovieCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<UpdateMovieCommandResponse> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = mapper.Map<Movie>(request);
            await unitOfWork.MoviesRepository.Update(movie, unitOfWork.Transaction);
            return mapper.Map<UpdateMovieCommandResponse>(movie);
        }
    }
}

