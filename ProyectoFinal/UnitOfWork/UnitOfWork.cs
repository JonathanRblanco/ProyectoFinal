using ProyectoFinal.Contracts;
using System.Data;

namespace ProyectoFinal.UnitOfWork
{
    /*El patrón IUnitOfWork es un patrón de diseño utilizado en el desarrollo de software para manejar transacciones en una aplicación.
     * IUnitOfWork se basa en la idea de que todas las operaciones de la aplicación deben estar encapsuladas en una transacción única
     * que se completa o revierte en su totalidad. Esto significa que, si alguna de las operaciones falla,
     * se deben deshacer todas las operaciones que se realizaron hasta ese momento.IUnitOfWork se implementa utilizando una interfaz
     * que define un conjunto de métodos que deben implementarse para iniciar, completar y revertir una transacción. 
     * Esto significa que las operaciones de la aplicación pueden realizarse sin tener que preocuparse por la transacción subyacente.
     * El patrón IUnitOfWork también puede ayudar a reducir la complejidad de la aplicación y mejorar la legibilidad del código al separar 
     * la lógica de la transacción de la lógica de la aplicación.*/
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly ILogger<UnitOfWork> _logger;
        private IDbTransaction _transaction = null;

        public UnitOfWork(IDbConnection connection,
            IUsuarioRepository usuarioRepository,
            IRolesRepository rolesRepository,
            IUserRolesRepository userRolesRepository,
            ILogger<UnitOfWork> logger)
        {
            _connection = connection;
            _usuarioRepository = usuarioRepository;
            _rolesRepository = rolesRepository;
            _userRolesRepository = userRolesRepository;
            _logger = logger;
            _connection.Open();
        }

        public IUsuarioRepository UsuarioRepository => _usuarioRepository;
        public IUserRolesRepository UserRolesRepository => _userRolesRepository;
        public IRolesRepository RolesRepository => _rolesRepository;
        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;


        public void BeginTransaction()
        {
            _logger.LogInformation("Comenzando transaccion");
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _logger.LogInformation("Confirmando transaccion");
            _transaction?.Commit();
            _transaction = null;

        }

        public void RollBack()
        {
            _logger.LogInformation("Retrocediendo transaccion");
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
