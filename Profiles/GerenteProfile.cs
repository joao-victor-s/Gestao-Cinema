using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIFilmes.Data.Dtos;
using APIFilmes.Models;

namespace APIFilmes.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            CreateMap<Gerente, ReadGerenteDto>()
                .ForMember(gerente => gerente.Cinemas, opts => opts
                .MapFrom(gerente => gerente.Cinemas.Select
                (cinema => new { cinema.Id, cinema.Nome, cinema.Endereco, cinema.EnderecoId } )));
        }
    }
}
