namespace ProyectoFinal.ServicesContracts
{
    public interface IMovieService
    {
        Task<string> CreateMovies(MultipartFormDataContent content);
        Task<string> Update(string data);
        Task<string> Delete(int id);
        Task<string> GetAll();
        Task<T> GetAll<T>();
        Task<string> GetMovieById(int id);
        Task<T> GetMovieById<T>(int id);
    }
}
