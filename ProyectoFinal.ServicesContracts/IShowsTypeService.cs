namespace ProyectoFinal.ServicesContracts
{
    public interface IShowsTypeService
    {
        Task<string> Create(string json);
        Task<string> Update(string json);
        Task<string> GetAll();
        Task<T> GetAll<T>();
        Task<string> GetById(int id);
        Task<T> GetById<T>(int id);
        Task<string> Delete(int id);
    }
}
