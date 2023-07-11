namespace ProyectoFinal.ServicesContracts
{
    public interface IReviewsService
    {
        Task<string> Create(string json);
        public Task<T> GetAll<T>();
        Task<string> GetAll();
    }
}
