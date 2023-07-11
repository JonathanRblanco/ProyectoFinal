using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Branches
{
    public class ModifyBranchHandler : IRequestHandler<ModifyBranchRequest, Result>
    {
        private readonly IBranchService branchService;
        public ModifyBranchHandler(IBranchService branchService)
        {
            this.branchService = branchService;
        }
        public async Task<Result> Handle(ModifyBranchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await branchService.Update(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
