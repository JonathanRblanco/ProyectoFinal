﻿using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ProyectoFinal.DTO.Requests
{
    public class CreateMovieRequest : IRequest<Result>
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public int GenderId { get; set; }
        public string Duration { get; set; }
        public int ClasificationId { get; set; }
        public string Actors { get; set; }
        public string Director { get; set; }

    }
}
