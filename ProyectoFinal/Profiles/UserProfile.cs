using AutoMapper;
using ProyectoFinal.DTOs.Requests;
using ProyectoFinal.Models;

namespace ProyectoFinal.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, User>();
        }
    }
}
