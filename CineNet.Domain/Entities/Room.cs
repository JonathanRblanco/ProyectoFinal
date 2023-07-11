namespace CineNet.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public int CinemaBranchId { get; set; }
        public Branch CinemaBranch { get; set; }
    }
}
