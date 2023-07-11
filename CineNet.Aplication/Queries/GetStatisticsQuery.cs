using MediatR;

namespace CineNet.Aplication.Queries
{
    public class GetStatisticsQuery : IRequest<IEnumerable<GetStatisticsQueryResponse>>
    {
        public int CinemaId { get; set; }
        public int Year { get; set; }
        public int? Branch { get; set; }
    }
}
