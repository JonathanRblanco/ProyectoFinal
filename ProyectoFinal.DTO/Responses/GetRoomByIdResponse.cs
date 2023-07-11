namespace ProyectoFinal.DTO.Responses
{
    public class GetRoomByIdResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public int CinemaBranchId { get; set; }
        public GetBranchByIdResponse CinemaBranch { get; set; }
    }
}
