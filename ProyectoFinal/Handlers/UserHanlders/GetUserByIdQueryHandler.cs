using AutoMapper;
using MediatR;
using ProyectoFinal.Contracts;
using ProyectoFinal.DTOs.Querys.Requests;
using ProyectoFinal.DTOs.Querys.Responses;
using ProyectoFinal.Extensions;
using System.Text.Json;

namespace ProyectoFinal.Handlers.UserHanlders
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, GetUserByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetUserByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetUserByIdQueryResponse();
            try
            {
                _logger.LogInformation($"Consultando UsuarioRepositorio: Request:{JsonSerializer.Serialize(request)}");
                var usuario = await _unitOfWork.UsuarioRepository.GetUserByIdAsync(request.Id.FromHashId());
                response = _mapper.Map<GetUserByIdQueryResponse>(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error! : {0} {1}", ex.Message, ex.StackTrace);
            }
            _logger.LogInformation($"Respuesta Handler:{JsonSerializer.Serialize(response)}");
            return response;
        }
    }
}
