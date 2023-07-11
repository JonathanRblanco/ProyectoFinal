using System.Data;

namespace ProyectoFinal.ServicesContracts
{
    public interface IUnitOfWork
    {
        public IDbTransaction Transaction { get; }
        public IDbConnection Connection { get; }
        IUsersRepository UsersRepository { get; }
        IRolesRepository RolesRepository { get; }
        IUserRolesRepository UserRolesRepository { get; }
        IUserLoginsRepository UsersLoginsRepository { get; }
        void CommitTransaction();
        void BeginTransaction();
        void RollBack();
    }
}
