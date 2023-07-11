using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;


namespace CineNet.Infraestructure.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly IDbConnection _connection;


        public MoviesRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(IDbTransaction transaction)
        {
            return await _connection.QueryAsync<Movie>(getAllMoviesQuery, transaction);
        }

        public async Task<int> CreateMovie(Movie movie, IDbTransaction transaction)
        {
            return await _connection.ExecuteScalarAsync<int>(createMovieQuery, movie, transaction);
        }

        public async Task Delete(int id, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteMovieQuery, new { Id = id }, transaction);
        }

        public async Task<Movie> GetMovieByIdAsync(int Id, IDbTransaction transaction)
        {

            return await _connection.QueryFirstOrDefaultAsync<Movie>(getMovieByIdQuery, new { Id = Id }, transaction);

        }

        public async Task Update(Movie movie, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(updateMovieQuery, movie, transaction);
        }

        private const string createMovieQuery = @"INSERT INTO [dbo].[Movies]
                                                   ([Name],
                                                    [Synopsis],
                                                    [GenderId],
                                                    [Duration],
                                                    [ClasificationId],
                                                    [Actors],
                                                    [Director],
                                                    [ImageId])
                                                VALUES
                                                   (@Name,
                                                    @Synopsis,
                                                    @GenderId,
                                                    @Duration,
                                                    @ClasificationId,
                                                    @Actors,
                                                    @Director,
                                                    @ImageId);
                                                    SELECT CAST(SCOPE_IDENTITY() AS int)";

        private const string getAllMoviesQuery = @"SELECT * From Movies";

        private const string deleteMovieQuery = @"DELETE FROM Movies WHERE Id = @Id";

        private const string getMovieByIdQuery = @"SELECT * 
                                                FROM Movies 
                                                WHERE Id = @Id";

        private const string updateMovieQuery = @"UPDATE Movies
                                                   SET  Name = @Name,
                                                        Synopsis = @Synopsis,
                                                        GenderId = @GenderId,
                                                        Duration = @Duration,
                                                        ClasificationId = @ClasificationId,
                                                        Actors = @Actors,
                                                        Director = @Director
                                                   WHERE Id = @Id";
    }
}
