using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ModifyUserRequest : IRequest<Result>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public IEnumerable<string> NewRoles { get; set; }
        public IEnumerable<string> OldRoles { get; set; }
    }
}
