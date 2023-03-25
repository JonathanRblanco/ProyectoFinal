using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.DTOs.Commands.Requests;

namespace ProyectoFinal.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserCommandRequest, IdentityUser>();
        }
    }
}
