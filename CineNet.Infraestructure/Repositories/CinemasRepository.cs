using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace ProyectoFinal.Repositorys
{
    public class CinemasRepository : ICinemasRepository
    {
        private readonly IDbConnection _connection;

        public CinemasRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Cinema>> GetAllCines(IDbTransaction transaction)
        {
            return await _connection.QueryAsync<Cinema>(getAllCinesQuery, transaction);
        }
        public async Task<int> CreateCine(Cinema cine, IDbTransaction transaction)
        {
            return await _connection.ExecuteScalarAsync<int>(createCineQuery, cine, transaction);
        }
        public async Task Delete(int id, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteCineQuery, new { Id = id }, transaction);
        }
        public async Task Update(Cinema cinema, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(updateCineQuery, cinema, transaction);
        }
        public async Task<Cinema> GetById(int id, IDbTransaction transaction)
        {
            return await _connection.QueryFirstOrDefaultAsync<Cinema>(getCinemaByIdQuery, new { Id = id }, transaction);
        }
        public async Task<IEnumerable<Cinema>> GetCinemasByUserId(string userId, IDbTransaction transaction)
        {
            return await _connection.QueryAsync<Cinema>(getCinemasByUserIdQuery, new { UserId = userId }, transaction);
        }

        public async Task DeleteCinemasAssignByUserId(string userId, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteCinemasAssignByUserIdquery, new { UserId = userId }, transaction);
        }
        public async Task InsertCinemasAssignByUserId(string userId, int cinemaId, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(insertCinemasAssignByUserIdquery, new { UserId = userId, CinemaId = cinemaId }, transaction);
        }

        public async Task<IEnumerable<Statistics>> GetStatistics(int cinemaId, int year, int? branch, IDbTransaction transaction)
        {
            return await _connection.QueryAsync<Statistics>(spGetStatistics, new { CinemaId = cinemaId, Year = year, Branch = branch }, transaction);
        }

        private const string getCinemaByIdQuery = @"SELECT * FROM [dbo].[Cinemas]
                                                        WHERE Id = @Id";
        private const string getAllCinesQuery = @"SELECT * 
                                                FROM Cinemas";
        private const string createCineQuery = @"INSERT INTO [dbo].[Cinemas]
                                                   ([Name]
                                                   ,[URL]
                                                   ,[Status])
                                                VALUES
                                                   (@Name,
                                                    @URL,
                                                    1);
                                                    SELECT CAST(SCOPE_IDENTITY() AS int)";
        private const string deleteCineQuery = @"DELETE FROM [dbo].[Cinemas]
                                                WHERE Id = @Id";
        private const string updateCineQuery = @"UPDATE [dbo].[Cinemas]
                                              SET [Name] = @Name,
                                                  [URL] = @URL, 
                                                  [Status] = @Status
                                              WHERE Id = @Id";
        private const string getCinemasByUserIdQuery = @"SELECT * from Cinemas c
                                                          INNER JOIN [UsersCinemas] uc ON uc.CinemaId = c.Id
                                                          WHERE uc.UserId = @UserId";

        public const string deleteCinemasAssignByUserIdquery = @"DELETE FROM [UsersCinemas]
                                                                 WHERE UserId = @UserId";
        public const string insertCinemasAssignByUserIdquery = @"INSERT INTO [dbo].[UsersCinemas]
                                                                    ([UserId]
                                                                    ,[CinemaId])
                                                                 VALUES
                                                                    (@UserId,
                                                                     @CinemaId)";
        public const string spGetStatistics = @"EXEC [StatisticsByCinema] @CinemaId,@Year,@Branch";
    }
}