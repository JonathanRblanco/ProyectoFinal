using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Branches
{
    public class DeleteBranchHandler : IRequestHandler<DeleteBranchRequest, Result>
    {
        private readonly IBranchService branchService;

        public DeleteBranchHandler(IBranchService branchService)
        {
            this.branchService = branchService;
        }
        public async Task<Result> Handle(DeleteBranchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await branchService.Delete(request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
