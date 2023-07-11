using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly IDbConnection connection;

        public ImagesRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<Image> GetById(int id, IDbTransaction transaction)
        {
            return await connection.QueryFirstOrDefaultAsync<Image>(getByIdQuery, new { Id = id }, transaction);
        }
        public async Task<int> Create(byte[] file, IDbTransaction transaction)
        {
            return await connection.ExecuteScalarAsync<int>(insertImageQuery, new { file = file }, transaction);
        }

        public async Task Update(int imageId, byte[] imageData, IDbTransaction transaction)
        {
            await connection.ExecuteAsync(updateQuery, new { Id = imageId, file = imageData }, transaction);
        }

        private readonly string getByIdQuery = @"SELECT * FROM Images
                                                WHERE Id=@Id";

        private readonly string insertImageQuery = @"INSERT INTO Images
                                                    VALUES(@file);
                                                    SELECT CAST(SCOPE_IDENTITY() AS int)";

        private readonly string updateQuery = @"UPDATE Images SET [File] = @file Where Id = @Id";
    }
}
