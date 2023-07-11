namespace ProyectoFinal.ServicesContracts
{
    public interface IClasificationsService
    {
        Task<string> GetAll();
        Task<T> GetAll<T>();
        Task<string> Create(string json);
        Task<string> Update(string json);
        Task<string> GetById(int id);
        Task<T> GetById<T>(int id);
        Task<string> Delete(int id);
    }
}
