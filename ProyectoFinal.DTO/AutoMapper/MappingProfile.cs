using AutoMapper;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, GetUsersResponse>()
                .ForMember(u => u.State, opciones => opciones.MapFrom(MapState));
            CreateMap<User, EditUserResponse>();
            CreateMap<User, SeeProfileResponse>();
            CreateMap<Rol, GetRolesResponse>();
            CreateMap<CreateRolRequest, Rol>();
            CreateMap<EditRolRequest, Rol>();
            CreateMap<Rol, EditRolResponse>();
            CreateMap<ModifyRolRequest, Rol>();
            CreateMap<DeleteRolRequest, Rol>();
        }
        private string MapState(User user, GetUsersResponse response)
        {
            return user.LockoutEnabled ? "Suspendido" : "Habilitado";
        }
    }
}
