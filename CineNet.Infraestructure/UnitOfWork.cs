using CineNet.Domain.Contracts;
using System.Data;

namespace CineNet.Infraestructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private readonly IBranchesRepository _branchesRepository;
        private readonly IMoviesRepository _moviesRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IGendersRepository _gendersRepository;
        private readonly IClasificationsRepository _clasificationsRepository;
        private readonly IShowsTypeRepository _showsTypeRepository;
        private readonly IShowsRepository _showsRepository;
        private readonly IReceiptsRepository _receiptsRepository;
        private readonly IEmailTasksRepository _emailTasksRepository;
        private readonly ICinemasRepository _cinemasRepository;
        private readonly IReviewsRepository _reviewsRepository;
        private IDbTransaction _transaction = null;

        public UnitOfWork(IDbConnection connection,
            ICinemasRepository cinemasRepository,
            IBranchesRepository branchesRepository,
            IMoviesRepository moviesRepository,
            IImagesRepository imagesRepository,
            IRoomsRepository roomsRepository,
            IGendersRepository gendersRepository,
            IClasificationsRepository clasificationsRepository,
            IShowsTypeRepository showsTypeRepository,
            IShowsRepository showsRepository,
            IReviewsRepository reviewsRepository,
            IReceiptsRepository receiptsRepository,
            IEmailTasksRepository emailTasksRepository)
        {
            _connection = connection;
            _branchesRepository = branchesRepository;
            _moviesRepository = moviesRepository;
            _imagesRepository = imagesRepository;
            _roomsRepository = roomsRepository;
            _gendersRepository = gendersRepository;
            _clasificationsRepository = clasificationsRepository;
            _showsTypeRepository = showsTypeRepository;
            _showsRepository = showsRepository;
            _receiptsRepository = receiptsRepository;
            _emailTasksRepository = emailTasksRepository;
            _cinemasRepository = cinemasRepository;
            _reviewsRepository = reviewsRepository;
            _connection.Open();
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;
        public IBranchesRepository BranchesRepository => _branchesRepository;
        public IMoviesRepository MoviesRepository => _moviesRepository;
        public ICinemasRepository CinemasRepository => _cinemasRepository;
        public IImagesRepository ImagesRepository => _imagesRepository;
        public IRoomsRepository RoomsRepository => _roomsRepository;
        public IGendersRepository GendersRepository => _gendersRepository;
        public IClasificationsRepository ClasificationsRepository => _clasificationsRepository;
        public IShowsTypeRepository ShowsTypeRepository => _showsTypeRepository;
        public IShowsRepository ShowsRepository => _showsRepository;
        public IReviewsRepository ReviewsRepository => _reviewsRepository;
        public IReceiptsRepository ReceiptsRepository => _receiptsRepository;
        public IEmailTasksRepository EmailTasksRepository => _emailTasksRepository;
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction = null;

        }

        public void RollBack()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            if (_connection != null)
            {
                _connection.Close();
            }
        }
        ~UnitOfWork()
        {
            Dispose();
        }
    }
}
