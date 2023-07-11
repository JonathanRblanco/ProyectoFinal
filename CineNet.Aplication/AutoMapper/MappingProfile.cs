using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Aplication.Queries;
using CineNet.Domain.Entities;

namespace CineNet.Aplication.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Statistics, GetStatisticsQueryResponse>();
            CreateMap<EmailTask, GetChangePasswordTasksQueryResponse>();
            CreateMap<EmailTask, GetEmailReceiptTasksQueryResponse>();
            CreateMap<EmailTask, GetConfirmEmailTasksQueryResponse>();
            CreateMap<Receipt, GetReceiptByIdQueryResponse>();
            CreateMap<CreateReceiptCommand, Receipt>();
            CreateMap<Receipt, CreateReceiptCommandResponse>();
            CreateMap<Receipt, GetReceiptsQueryResponse>();
            CreateMap<Room, GetRoomsQueryResponse>();
            CreateMap<Movie, GetMoviesQueryResponse>();
            CreateMap<CreateMovieCommand, Movie>();
            CreateMap<CreateCinemaCommand, Cinema>();
            CreateMap<Movie, CreateMovieCommandResponse>();
            CreateMap<Cinema, CreateCinemaCommandResponse>();
            CreateMap<CreateBranchCommand, Branch>();
            CreateMap<Branch, CreateBranchCommandResponse>();
            CreateMap<Branch, GetBranchesQueryResponse>();
            CreateMap<Image, GetImageQueryResponse>();
            CreateMap<Gender, GetGendersQueryResponse>();
            CreateMap<Cinema, GetCinemasQueryResponse>();
            CreateMap<Clasification, GetClasificationsQueryResponse>();
            CreateMap<Cinema, GetCinemasQueryResponse>();
            CreateMap<CreateGenderCommand, Gender>();
            CreateMap<Gender, CreateGenderCommandResponse>();
            CreateMap<Gender, GetGenderByIdQueryResponse>();
            CreateMap<UpdateGenderCommand, Gender>();
            CreateMap<Gender, UpdateGenderCommandResponse>();
            CreateMap<CreateRoomCommand, Room>();
            CreateMap<Room, CreateRoomCommandResponse>();
            CreateMap<Room, GetRoomByIdQueryResponse>();
            CreateMap<UpdateRoomCommand, Room>();
            CreateMap<Room, UpdateRoomCommandResponse>();
            CreateMap<CreateClasificationCommand, Clasification>();
            CreateMap<Clasification, CreateClasificationCommandResponse>();
            CreateMap<Clasification, GetClasificationByIdQueryResponse>();
            CreateMap<UpdateClasificationCommand, Clasification>();
            CreateMap<Clasification, UpdateClasificationCommandResponse>();
            CreateMap<ShowType, GetShowsTypeQueryResponse>();
            CreateMap<UpdateMovieCommand, Movie>();
            CreateMap<ShowType, GetShowTypeByIdQueryResponse>();
            CreateMap<Movie, UpdateMovieCommandResponse>();
            CreateMap<CreateShowTypeCommand, ShowType>();
            CreateMap<ShowType, CreateShowTypeCommandResponse>();
            CreateMap<UpdateShowTypeCommand, ShowType>();
            CreateMap<ShowType, UpdateShowTypeCommandResponse>();
            CreateMap<CreateShowCommand, Show>();
            CreateMap<Show, CreateShowCommandResponse>();
            CreateMap<UpdateShowCommand, Show>();
            CreateMap<Show, UpdateShowCommandResponse>();
            CreateMap<Show, GetShowsQueryResponse>();
            CreateMap<Show, GetShowByIdQueryResponse>();
            CreateMap<UpdateCinemaCommand, Cinema>();
            CreateMap<Cinema, GetCinemaByIdQueryResponse>();
            CreateMap<Cinema, UpdateCinemaCommandResponse>();
            CreateMap<Branch, GetBranchByIdQueryResponse>();
            CreateMap<Branch, UpdateBranchCommandResponse>();
            CreateMap<UpdateBranchCommand, Branch>();
            CreateMap<Movie, GetMovieByIdQueryResponse>();
            CreateMap<Review, GetReviewsQueryResponse>();
            CreateMap<Cinema, GetCinemasByUserIdQueryResponse>();
            CreateMap<CreateReviewCommand, Review>();
            CreateMap<Review, CreateReviewCommandResponse>();
        }
    }
}
