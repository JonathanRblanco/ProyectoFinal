using Dapper;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Contracts;
using ProyectoFinal.Models;
using System.Data;

namespace ProyectoFinal.Repositorys
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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _connection;

        public UsuarioRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<Usuario> GetUserByIdAsync(int Id)
        {
            return await _connection.QueryFirstAsync<Usuario>(getUserByIdQuery, new { Id });
        }
        public async Task<int> CreateUser(IdentityUser user, IDbTransaction transaction)
        {
            return await _connection.ExecuteAsync(createUsuQuery, user, transaction);
        }
        //TODO : IMPLEMENTAR LOS METODOS NECESARIOS DEL REPOSITORIO
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
    }
}
