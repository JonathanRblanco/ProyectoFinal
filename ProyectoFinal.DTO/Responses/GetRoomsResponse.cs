namespace ProyectoFinal.DTO.Responses
{
    public class GetRoomsResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public int CinemaBranchId { get; set; }
        public GetBranchesResponse CinemaBranch { get; set; }
    }
}
