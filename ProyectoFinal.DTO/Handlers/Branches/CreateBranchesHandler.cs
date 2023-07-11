using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Branches
{
    public class CreateBranchesHandler : IRequestHandler<CreateBranchRequest, Result>
    {
        private readonly IBranchService _branchService;
        public CreateBranchesHandler(IBranchService branchService)
        {
            _branchService = branchService;
        }
        public async Task<Result> Handle(CreateBranchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await _branchService.Create(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error!:{ex.Message}");
            }
        }
    }
}
