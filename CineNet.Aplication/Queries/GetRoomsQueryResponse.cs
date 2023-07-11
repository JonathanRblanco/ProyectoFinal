namespace CineNet.Aplication.Queries
{
    public class GetRoomsQueryResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public int CinemaBranchId { get; set; }
        public GetBranchesQueryResponse CinemaBranch { get; set; }
    }
}
