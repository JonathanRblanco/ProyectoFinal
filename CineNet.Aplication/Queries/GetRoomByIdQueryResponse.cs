namespace CineNet.Aplication.Queries
{
    public class GetRoomByIdQueryResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public int CinemaBranchId { get; set; }
        public GetBranchByIdQueryResponse CinemaBranch { get; set; }
    }
}
