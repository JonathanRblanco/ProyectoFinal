using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Branches
{
    public class GetBranchesHandler : IRequestHandler<GetBranchesRequest, Result<IEnumerable<GetBranchesResponse>>>
    {
        private readonly IBranchService _branchService;
        public GetBranchesHandler(IBranchService branchService)
        {
            _branchService = branchService;
        }
        public async Task<Result<IEnumerable<GetBranchesResponse>>> Handle(GetBranchesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var branches = await _branchService.GetAll<IEnumerable<GetBranchesResponse>>();
                if (request.CinemaId.HasValue)
                {
                    branches = branches.Where(b => b.CineId == request.CinemaId.Value);
                }
                return Result.Success(branches);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
