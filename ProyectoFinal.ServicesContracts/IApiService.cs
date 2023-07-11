namespace ProyectoFinal.ServicesContracts
{
    public interface IApiService
    {
        Task<string> DeleteAsync(string url);
        Task<string> GetAsync(string url);
        Task<string> PostAsync(string url, MultipartFormDataContent content);
        Task<string> PostJsonAsync(string url, string jsonData);
        Task<string> PutJsonAsync(string url, string jsonData);
        Task<string> PutAsync(string url, MultipartFormDataContent content);

    }
}
