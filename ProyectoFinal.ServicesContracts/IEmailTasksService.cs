namespace ProyectoFinal.ServicesContracts
{
    public interface IEmailTasksService
    {
        Task<string> CreateTask(string json);
    }
}
