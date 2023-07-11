using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class BranchesRepository : IBranchesRepository
    {
        private readonly IDbConnection _connection;

        public BranchesRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> CreateBranch(Branch cinemaBranch, IDbTransaction transaction)
        {
            return await _connection.ExecuteScalarAsync<int>(createBranchQuery, cinemaBranch, transaction);
        }

        public async Task Delete(int id, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteBranchesQuery, new { Id = id }, transaction);

        }

        public async Task<IEnumerable<Branch>> GetAllBranches(IDbTransaction transaction)
        {
            return await _connection.QueryAsync<Branch>(getAllBranchesQuery);
        }
        public async Task<Branch> GetById(int id, IDbTransaction transaction)
        {
            return await _connection.QueryFirstOrDefaultAsync<Branch>(getBranchByIdQuery, new { Id = id }, transaction);
        }
        public async Task Update(Branch branch, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(updateBranchByIdQuery, branch, transaction);
        }


        public const string createBranchQuery = @"INSERT INTO [dbo].[CinemaBranchs]
                                                           ([CineId]
                                                           ,[Name]
                                                           ,[Address]
                                                           ,[Location]
                                                           ,[Province]
                                                           ,[CP]
                                                           ,[Email]
                                                           ,[Phone])
                                                       VALUES
                                                           (@CineId
                                                           ,@Name
                                                           ,@Address
                                                           ,@Location
                                                           ,@Province
                                                           ,@CP
                                                           ,@Email
                                                           ,@Phone);
                                                       SELECT CAST (SCOPE_IDENTITY() AS int)";

        public const string getAllBranchesQuery = @"SELECT * 
                                                         FROM CinemaBranchs";

        private const string deleteBranchesQuery = @"DELETE FROM CinemaBranchs 
                                                          WHERE Id = @Id";
        public const string getBranchByIdQuery = @"SELECT *
                                                         FROM CinemaBranchs
                                                         WHERE Id = @Id";
        public const string updateBranchByIdQuery = @"UPDATE CinemaBranchs
                                                    SET CineId = @CineId,
                                                        Name = @Name,
                                                        Address = @Address,
                                                        Location = @Location,
                                                        Province = @Province,
                                                        CP = @CP,
                                                        Email = @Email,
                                                        Phone = @Phone
                                                    WHERE Id = @Id";
    }
}
