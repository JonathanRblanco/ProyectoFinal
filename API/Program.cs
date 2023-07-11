using CineNet.Aplication.AutoMapper;
using CineNet.Aplication.Hanlders;
using CineNet.Domain.Contracts;
using CineNet.Infraestructure;
using CineNet.Infraestructure.Repositories;
using Microsoft.Data.SqlClient;
using ProyectoFinal.Repositorys;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;

#region Services
services.AddScoped<IDbConnection>(x => new SqlConnection(builder.Configuration.GetConnectionString("AppContext")));
services.AddScoped<ICinemasRepository, CinemasRepository>();
services.AddScoped<IBranchesRepository, BranchesRepository>();
services.AddScoped<IMoviesRepository, MoviesRepository>();
services.AddScoped<IImagesRepository, ImagesRepository>();
services.AddScoped<IRoomsRepository, RoomsRepository>();
services.AddScoped<IGendersRepository, GendersRepository>();
services.AddScoped<IClasificationsRepository, ClasificationsRepository>();
services.AddScoped<IShowsTypeRepository, ShowsTypeRepository>();
services.AddScoped<IShowsRepository, ShowsRepository>();
services.AddScoped<IReceiptsRepository, ReceiptsRepository>();
services.AddScoped<IReviewsRepository, ReviewsRepository>();
services.AddScoped<IEmailTasksRepository, EmailTasksRepository>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region AutoMapper
services.AddAutoMapper(configure => configure.AddProfile<MappingProfile>(), typeof(Program));
#endregion

#region MediatR
services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<GetCinemasQueryHandler>());
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
