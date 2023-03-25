using Dapper;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Contracts;

namespace ProyectoFinal.Repositorys
{
    //TODO : IMPLEMENTAR LOS METODOS DEL STORE USANDO LOS REPOSITORIOS DE IUNITOFWORK
    public class UserStore : IUserStore<IdentityUser>, IUserPasswordStore<IdentityUser>, IUserEmailStore<IdentityUser>, IUserRoleStore<IdentityUser>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddToRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener el ID del rol utilizando Dapper y la instancia de IUnitOfWork
                var roleId = await _unitOfWork.RolesRepository.GetRoleByRoleName(roleName, _unitOfWork.Transaction);

                if (roleId is null)
                {
                    throw new ArgumentException($"El rol '{roleName}' no existe.");
                }
                // Asociar el usuario con el rol en la tabla UserRoles utilizando Dapper y la instancia de IUnitOfWork
                await _unitOfWork.UserRolesRepository.InsertUsuRol(roleId, user.Id, _unitOfWork.Transaction);
                // Completar la transacción
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al asociar el usuario con el rol
                throw new Exception($"Error al agregar el usuario al rol '{roleName}': {ex.Message}", ex);
            }
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            try
            {
                // Agregar el usuario a la base de datos utilizando Dapper y la instancia de IUnitOfWork
                _unitOfWork.BeginTransaction();
                user.ConcurrencyStamp = Guid.NewGuid().ToString();
                user.SecurityStamp = Guid.NewGuid().ToString();
                await _unitOfWork.UsuarioRepository.CreateUser(user, _unitOfWork.Transaction);
                await AddToRoleAsync(user, "Usu", default);
                // Completar la transacción
                _unitOfWork.CommitTransaction();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al agregar el usuario a la base de datos
                _unitOfWork.RollBack();
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            try
            {
                // Eliminar el usuario de la base de datos utilizando Dapper y la instancia de IUnitOfWork
                await _unitOfWork.Connection.ExecuteAsync(
                    "DELETE FROM Users WHERE Id = @Id",
                    new { user.Id },
                    _unitOfWork.Transaction);

                // Completar la transacción
                _unitOfWork.CommitTransaction();

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al eliminar el usuario de la base de datos
                _unitOfWork.RollBack();
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<IdentityUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            // Buscar al usuario por su email normalizado utilizando Dapper y la instancia de IUnitOfWork
            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<IdentityUser>(
                "SELECT * FROM Users WHERE NormalizedEmail = @NormalizedEmail",
                new { NormalizedEmail = normalizedEmail },
                _unitOfWork.Transaction);
        }

        public async Task<IdentityUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            // Obtener el usuario de la base de datos utilizando Dapper y la instancia de IUnitOfWork
            var result = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<IdentityUser>(
                "SELECT * FROM Users WHERE Id = @Id",
                new { Id = userId },
                _unitOfWork.Transaction);

            return result;
        }

        public async Task<IdentityUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            // Obtener el usuario de la base de datos utilizando Dapper y la instancia de IUnitOfWork
            var result = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<IdentityUser>(
                "SELECT * FROM Users WHERE UPPER(UserName) = UPPER(@UserName)",
                new { UserName = normalizedUserName },
                _unitOfWork.Transaction);

            return result;
        }

        public async Task<string?> GetEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // Devolver el email del usuario
            return await Task.FromResult(user.Email);
        }

        public async Task<bool> GetEmailConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // Devolver true si el email del usuario está confirmado, false de lo contrario
            return await Task.FromResult(user.EmailConfirmed);
        }

        public async Task<string?> GetNormalizedEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedEmail);
        }

        public async Task<string?> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // Devolver el nombre de usuario normalizado
            return await Task.FromResult(user.UserName?.ToUpper());
        }

        public async Task<string?> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // Devolver el passwordHash del usuario
            return await Task.FromResult(user.PasswordHash);
        }

        public async Task<IList<string>> GetRolesAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // Obtener los nombres de los roles asociados al usuario utilizando Dapper y la instancia de IUnitOfWork
            var result = await _unitOfWork.Connection.QueryAsync<string>(
                "SELECT r.Name FROM UserRoles ur INNER JOIN Roles r ON ur.RoleId = r.Id WHERE ur.UserId = @UserId",
                new { UserId = user.Id },
                _unitOfWork.Transaction);

            return result.ToList();
        }

        public async Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // Devolver el ID del usuario
            return await Task.FromResult(user.Id);
        }

        public async Task<string?> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // Devolver el nombre de usuario
            return await Task.FromResult(user.UserName);
        }

        public async Task<IList<IdentityUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            // Obtener los usuarios asociados al rol utilizando Dapper y la instancia de IUnitOfWork
            var result = await _unitOfWork.Connection.QueryAsync<IdentityUser>(
                "SELECT u.* FROM Users u INNER JOIN UserRoles ur ON u.Id = ur.UserId \" +\r\n\"INNER JOIN Roles r ON ur.RoleId = r.Id WHERE UPPER(r.NormalizedName) = UPPER(@NormalizedName)",
                new { NormalizedName = roleName }, _unitOfWork.Transaction);
            return result.ToList();
        }

        public async Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // Devolver true si el usuario tiene un passwordHash, false de lo contrario
            return await Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public async Task<bool> IsInRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            // Verificar si el usuario está asociado con el rol utilizando Dapper y la instancia de IUnitOfWork
            var result = await _unitOfWork.Connection.ExecuteScalarAsync<bool>(
                "SELECT COUNT(*) FROM UserRoles ur INNER JOIN Roles r ON ur.RoleId = r.Id " +
                "WHERE ur.UserId = @UserId AND UPPER(r.NormalizedName) = UPPER(@NormalizedName)",
                new { UserId = user.Id, NormalizedName = roleName },
                _unitOfWork.Transaction);

            return result;
        }

        public async Task RemoveFromRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener el ID del rol utilizando Dapper y la instancia de IUnitOfWork
                var roleId = await _unitOfWork.Connection.ExecuteScalarAsync<int>(
                    "SELECT Id FROM Roles WHERE UPPER(NormalizedName) = UPPER(@NormalizedName)",
                    new { NormalizedName = roleName },
                    _unitOfWork.Transaction);

                if (roleId == default(int))
                {
                    // Si el rol no existe, lanzar una excepción
                    throw new ArgumentException($"El rol '{roleName}' no existe.");
                }

                // Remover la asociación del usuario con el rol en la tabla UserRoles utilizando Dapper y la instancia de IUnitOfWork
                await _unitOfWork.Connection.ExecuteAsync(
                    "DELETE FROM UserRoles WHERE UserId = @UserId AND RoleId = @RoleId",
                    new { UserId = user.Id, RoleId = roleId },
                    _unitOfWork.Transaction);

                // Completar la transacción
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al remover la asociación del usuario con el rol
                _unitOfWork.RollBack();
                throw new Exception($"Error al remover el usuario del rol '{roleName}': {ex.Message}", ex);
            }
        }

        public async Task SetEmailAsync(IdentityUser user, string? email, CancellationToken cancellationToken)
        {
            // Asignar el email al usuario y devolver una tarea completada
            user.Email = email;
            await Task.CompletedTask;
        }

        public async Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            // Asignar el valor confirmed a la propiedad EmailConfirmed del usuario y devolver una tarea completada
            user.EmailConfirmed = confirmed;
            await Task.CompletedTask;
        }

        public async Task SetNormalizedEmailAsync(IdentityUser user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            // Establecer el email normalizado del usuario
            user.NormalizedEmail = normalizedEmail;
            await Task.CompletedTask;
        }

        public async Task SetNormalizedUserNameAsync(IdentityUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            // Establecer el nombre de usuario normalizado en la instancia de ApplicationUser
            user.NormalizedUserName = normalizedName;
            await Task.CompletedTask;
        }

        public async Task SetPasswordHashAsync(IdentityUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            // Asignar el passwordHash al usuario y devolver una tarea completada
            user.PasswordHash = passwordHash;
            await Task.CompletedTask;
        }

        public async Task SetUserNameAsync(IdentityUser user, string? userName, CancellationToken cancellationToken)
        {
            // Establecer el nombre de usuario en la instancia de ApplicationUser
            user.UserName = userName;

            await Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            try
            {
                // Actualizar el usuario en la base de datos utilizando Dapper y la instancia de IUnitOfWork
                await _unitOfWork.Connection.ExecuteAsync(
                    "UPDATE Users SET UserName = @UserName, Email = @Email, PasswordHash = @PasswordHash " +
                    "WHERE Id = @Id",
                    new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.PasswordHash
                    },
                    _unitOfWork.Transaction);

                // Actualizar la instancia de ApplicationUser con los datos que se actualizaron en la base de datos
                user.ConcurrencyStamp = Guid.NewGuid().ToString();

                // Completar la transacción
                _unitOfWork.CommitTransaction();

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al actualizar el usuario en la base de datos
                _unitOfWork.RollBack();
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
