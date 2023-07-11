using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IUnitOfWork
    {
        public IDbTransaction Transaction { get; }
        public IDbConnection Connection { get; }
        public IBranchesRepository BranchesRepository { get; }
        public ICinemasRepository CinemasRepository { get; }
        public IMoviesRepository MoviesRepository { get; }
        public IImagesRepository ImagesRepository { get; }
        public IRoomsRepository RoomsRepository { get; }
        public IGendersRepository GendersRepository { get; }
        public IClasificationsRepository ClasificationsRepository { get; }
        public IShowsTypeRepository ShowsTypeRepository { get; }
        public IShowsRepository ShowsRepository { get; }
        public IReviewsRepository ReviewsRepository { get; }
        IReceiptsRepository ReceiptsRepository { get; }
        IEmailTasksRepository EmailTasksRepository { get; }

        void CommitTransaction();
        void BeginTransaction();
        void RollBack();
    }
}
