﻿namespace CineNet.Aplication.Queries
{
    public class GetBranchesQueryResponse
    {
        public int Id { get; set; }
        public int CineId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Province { get; set; }
        public string CP { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public GetCinemasQueryResponse Cinema { get; set; }
    }
}
