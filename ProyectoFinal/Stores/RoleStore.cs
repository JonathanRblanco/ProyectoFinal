using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.Stores
{
    //TODO : IMPLEMENTAR LOS METODOS DEL STORE UTILIZANDO EL REPOSITORIO DE ROLES DE IUNITOFWORK
    public class RoleStore : IRoleStore<Rol>, IQueryableRoleStore<Rol>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IQueryable<Rol> Roles => _unitOfWork.RolesRepository.GetAll(_unitOfWork.Transaction);

        public RoleStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> CreateAsync(Rol role, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.RolesRepository.Create(role, _unitOfWork.Transaction);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<IdentityResult> DeleteAsync(Rol role, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.RolesRepository.Delete(role.Id, _unitOfWork.Transaction);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<Rol?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RolesRepository.GetRoleById(roleId, _unitOfWork.Transaction);
        }

        public async Task<Rol?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RolesRepository.GetRoleByName(normalizedRoleName, _unitOfWork.Transaction);
        }

        public async Task<string?> GetNormalizedRoleNameAsync(Rol role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.NormalizedName);
        }

        public async Task<string> GetRoleIdAsync(Rol role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.Id);
        }

        public async Task<string?> GetRoleNameAsync(Rol role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.Name);
        }

        public async Task SetNormalizedRoleNameAsync(Rol role, string? normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            await Task.CompletedTask;
        }

        public async Task SetRoleNameAsync(Rol role, string? roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            await Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Rol role, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.RolesRepository.Update(role, _unitOfWork.Transaction);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }
        public void Dispose()
        {
            //_unitOfWork.Dispose();
        }
    }
}
