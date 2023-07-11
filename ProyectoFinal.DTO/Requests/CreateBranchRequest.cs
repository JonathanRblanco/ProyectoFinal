using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    // esta es la peticion     esta es la respuesta
    public class CreateBranchRequest : IRequest<Result>
    {
        //campos obligatorios para crear al branch, se llaman igual que los name del input que estan en la vista create
        public string Name { get; set; }
        public int CineId { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Province { get; set; }
        public string CP { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
