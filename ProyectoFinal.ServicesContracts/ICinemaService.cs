namespace ProyectoFinal.ServicesContracts
{
    public interface ICinemaService
    {
        Task<string> Create(string json);
        Task<string> Update(string json);
        Task<string> Delete(int id);
        public Task<T> GetAll<T>();
        Task<string> GetAll();
        Task<string> GetById(int id);
        Task<T> GetById<T>(int id);
        Task<T> GetCinemasByUserId<T>(string userId);
        Task<string> ModifyAssignCinema(string json);
        Task<string> GetCinemasByUserId(string userId);
        Task<string> getStatistics(int cinemaId, int year, int? branchId);
        Task<T> getStatistics<T>(int cinemaId, int year, int? branchId);
    }
}
