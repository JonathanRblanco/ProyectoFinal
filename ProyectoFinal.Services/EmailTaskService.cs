using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.Services
{
    public class EmailTaskService : IEmailTasksService
    {
        private readonly IApiService apiService;

        public EmailTaskService(IApiService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<string> CreateTask(string json)
        {
            return await apiService.PostJsonAsync("Tasks/Emails/", json);
        }
    }
}
