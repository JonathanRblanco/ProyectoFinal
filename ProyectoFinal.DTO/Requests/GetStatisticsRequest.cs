using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.DTO.Requests
{
    public class GetStatisticsRequest : IRequest<Result<IEnumerable<GetStatisticsResponse>>>
    {
        public int CinemaId { get; set; }
        public int Year { get; set; }
        public int? BranchId { get; set; }
    }
}
