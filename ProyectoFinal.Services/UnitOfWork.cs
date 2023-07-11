using Microsoft.Extensions.Logging;
using ProyectoFinal.ServicesContracts;
using System.Data;

namespace ProyectoFinal.Services
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
        private readonly IUsersRepository _usersRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IUserLoginsRepository _userLoginsRepository;
        private readonly ILogger<UnitOfWork> _logger;
        private IDbTransaction _transaction = null;

        public UnitOfWork(IDbConnection connection,
            IUsersRepository usersRepository,
            IRolesRepository rolesRepository,
            IUserRolesRepository userRolesRepository,
            IUserLoginsRepository userLoginsRepository,
            ILogger<UnitOfWork> logger)
        {
            _connection = connection;
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _userRolesRepository = userRolesRepository;
            _userLoginsRepository = userLoginsRepository;
            _logger = logger;
            _connection.Open();
        }

        public IUsersRepository UsersRepository => _usersRepository;
        public IUserRolesRepository UserRolesRepository => _userRolesRepository;
        public IRolesRepository RolesRepository => _rolesRepository;
        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;
        public IUserLoginsRepository UsersLoginsRepository => _userLoginsRepository;

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
                _transaction.Dispose();
            }

            if (_connection != null)
            {
                _connection.Close();
            }
        }
        ~UnitOfWork()
        {
            Dispose();
        }
    }
}
