﻿using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteBranchRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
