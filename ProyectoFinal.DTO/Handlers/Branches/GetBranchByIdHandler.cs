using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Branches
{
    public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdRequest, Result<GetBranchByIdResponse>>
    {
        private readonly IBranchService branchService;

        public GetBranchByIdHandler(IBranchService branchService)
        {
            this.branchService = branchService;
        }
        public async Task<Result<GetBranchByIdResponse>> Handle(GetBranchByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var branch = await branchService.GetById<GetBranchByIdResponse>(request.Id);
                if (branch == null)
                {
                    return Result.Error("La sucursal no existe");
                }
                return Result.Success(branch);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}