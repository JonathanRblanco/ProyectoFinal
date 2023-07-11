using MediatR;

namespace CineNet.Aplication.Commands
{
    public class CreateBranchCommand : IRequest<CreateBranchCommandResponse>
    {
        //propiedades para poder crear (lo mismo que me trae el formulario)
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
