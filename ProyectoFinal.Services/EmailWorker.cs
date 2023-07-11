using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.Services
{
    public class EmailWorker : BackgroundService
    {
        private readonly ILogger<EmailWorker> _logger;
        private readonly IServiceProvider serviceProvider;

        public EmailWorker(ILogger<EmailWorker> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    using var scope = serviceProvider.CreateScope();
                    var services = scope.ServiceProvider;

                    var emailTaskServices = services.GetServices<ITasksService>() ?? throw new InvalidOperationException("No se pudo construir el servicio del tipo: " + nameof(ITasksService));
                    foreach (var service in emailTaskServices)
                    {
                        await service.ExecuteTask();
                    }
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Worker failed " + ex.Message);
                }
                await Task.Delay(90000, stoppingToken); // Espera 1 minuto y medio
            }
        }
    }
}
