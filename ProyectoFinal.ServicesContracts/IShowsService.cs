namespace ProyectoFinal.ServicesContracts
{
    public interface IShowsService
    {
        Task<string> GetAll();
        Task<T> GetAll<T>();
        Task<string> Create(string data);
        Task<string> GetById(int id);
        Task<T> GetById<T>(int id);
        Task<string> Update(string json);
        Task Delete(int id);
    }
}
