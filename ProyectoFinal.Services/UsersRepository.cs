using Dapper;
using ProyectoFinal.Data;
using ProyectoFinal.ServicesContracts;
using System.Data;

namespace ProyectoFinal.Services
{
    /*El patrón Repository es un patrón de diseño de software que se utiliza comúnmente
     * en el desarrollo de aplicaciones para separar la lógica empresarial de la lógica de acceso a datos.
     * En términos simples, el patrón Repository proporciona una interfaz común para acceder a los datos 
     * almacenados en una base de datos o en otro origen de datos. Esta interfaz común se utiliza para
     * realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) en los datos.El patrón Repository
     * utiliza una clase o interfaz que representa la entidad de la base de datos y proporciona métodos
     * para realizar las operaciones CRUD en la entidad. El uso de esta interfaz permite que el código 
     * de lógica empresarial se separe del código de acceso a datos, lo que facilita la prueba 
     * y el mantenimiento de la aplicación.Además, el patrón Repository puede proporcionar una capa 
     * de abstracción adicional que permite cambiar el origen de datos subyacente sin cambiar el código de la lógica empresarial.
     * Esto puede ser útil si se desea cambiar de una base de datos relacional a una base de datos NoSQL o a un servicio web, por ejemplo.*/
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnection _connection;

        public UsersRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<User> GetUserByIdAsync(Guid Id, IDbTransaction transaction)
        {
            return await _connection.QueryFirstAsync<User>(getUserByIdQuery, new { Id });
        }
        public async Task<int> CreateUser(User user, IDbTransaction transaction)
        {
            return await _connection.ExecuteAsync(createUsuQuery, user, transaction);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(IDbTransaction transaction)
        {
            return await _connection.QueryAsync<User>(getAllUsersQuery);
        }
        public async Task Delete(string id, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteUserQuery, new { Id = id }, transaction);
        }

        public async Task ChangeImage(int imageId, string userId, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(changeImageQuery, new { ImageId = imageId, Id = userId });
        }

        public async Task<User> FindByEmail(string normalizedEmail, IDbTransaction transaction)
        {
            return await _connection.QueryFirstOrDefaultAsync<User>(findByEmailQuery, new { NormalizedEmail = normalizedEmail }, transaction);
        }

        public async Task<User> GetUserByLogin(string loginProvider, string providerKey, IDbTransaction transaction)
        {
            return await _connection.QueryFirstOrDefaultAsync<User>(getUserByLoginQuery, new { loginProvider = loginProvider, providerKey = providerKey }, transaction);
        }

        private const string getAllUsersQuery = @"SELECT * 
                                                FROM USERS";
        private const string createUsuQuery = @"INSERT INTO [dbo].[Users]
                                                   ([Id]
                                                   ,[UserName]
                                                   ,[NormalizedUserName]
                                                   ,[Email]
                                                   ,[NormalizedEmail]
                                                   ,[EmailConfirmed]
                                                   ,[PasswordHash]
                                                   ,[SecurityStamp]
                                                   ,[ConcurrencyStamp]
                                                   ,[PhoneNumber]
                                                   ,[PhoneNumberConfirmed]
                                                   ,[TwoFactorEnabled]
                                                   ,[LockoutEnd]
                                                   ,[LockoutEnabled]
                                                   ,[AccessFailedCount])
                                                VALUES
                                                   (@Id,
                                                    @UserName,
                                                    @NormalizedUserName,
                                                    @Email,
                                                    @NormalizedEmail,
                                                    @EmailConfirmed,
                                                    @PasswordHash,
                                                    @SecurityStamp,
                                                    @ConcurrencyStamp,
                                                    @PhoneNumber,
                                                    @PhoneNumberConfirmed,
                                                    @TwoFactorEnabled,
                                                    @LockoutEnd,
                                                    @LockoutEnabled,
                                                    @AccessFailedCount)";
        private const string getUserByIdQuery = @"SELECT * 
                                                FROM Users 
                                                WHERE Id = @Id";
        private const string deleteUserQuery = @"DELETE
                                                FROM USERS
                                                WHERE Id = @Id";
        private const string changeImageQuery = @"UPDATE USERS SET ImageId = @ImageId 
                                                WHERE Id = @Id";
        private const string findByEmailQuery = @"SELECT * FROM Users 
                                                WHERE NormalizedEmail = @NormalizedEmail";
        private const string getUserByLoginQuery = @"SELECT * FROM Users u
                                                    INNER JOIN UserLogins ul on ul.UserId = u.Id
                                                    WHERE ul.LoginProvider = @loginProvider
                                                    AND ul.ProviderKey = @providerKey";
    }
}
