using MediatR;

namespace ProyectoFinal.Loggin
{
    public class LogBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LogBehaviour<TRequest, TResponse>> _logger;
        public LogBehaviour(ILogger<LogBehaviour<TRequest, TResponse>> logger)
            => _logger = logger;
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Manejando {typeof(TRequest).Name}");
            var response = await next();

            _logger.LogInformation($"Manejado {typeof(TResponse).Name}");

            return response;
        }
    }
}
