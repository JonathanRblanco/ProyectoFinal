using System.Data;

namespace ProyectoFinal.Contracts
{
    public interface IUnitOfWork
    {
        public IDbTransaction Transaction { get; }
        public IDbConnection Connection { get; }
        IUsuarioRepository UsuarioRepository { get; }
        IRolesRepository RolesRepository { get; }
        IUserRolesRepository UserRolesRepository { get; }
        void CommitTransaction();
        void BeginTransaction();
        void RollBack();
        void Dispose();
    }
}
