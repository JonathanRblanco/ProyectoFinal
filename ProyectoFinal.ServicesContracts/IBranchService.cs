namespace ProyectoFinal.ServicesContracts
{
    public interface IBranchService
    {
        public Task<T> GetAll<T>();
        Task<string> GetAll();
        Task<string> Create(string data);
        Task<string> GetById(int id);
        Task<T> GetById<T>(int id);
        Task<string> Update(string json);
        Task Delete(int id);
    }
}
