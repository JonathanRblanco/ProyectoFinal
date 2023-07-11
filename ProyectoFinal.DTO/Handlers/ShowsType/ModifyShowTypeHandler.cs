﻿using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.ShowsType
{
    public class ModifyShowTypeHandler : IRequestHandler<ModifyShowTypeRequest, Result>
    {
        private readonly IShowsTypeService showsTypeService;

        public ModifyShowTypeHandler(IShowsTypeService showsTypeService)
        {
            this.showsTypeService = showsTypeService;
        }
        public async Task<Result> Handle(ModifyShowTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await showsTypeService.Update(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
