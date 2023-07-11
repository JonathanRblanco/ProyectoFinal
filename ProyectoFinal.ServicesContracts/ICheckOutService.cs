namespace ProyectoFinal.ServicesContracts
{
    public interface ICheckOutService
    {
        public Task<T> GetAll<T>();
        Task<string> GetAll();
        Task<string> Create(string data);
        Task<T> Create<T>(string data);
        Task<string> GetById(int id);
        Task<T> GetById<T>(int id);
    }
}
