using APIFilmes.Data;
using APIFilmes.Data.Dtos;
using APIFilmes.Models;
using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFilmes.Services
{
    public class GerenteService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public GerenteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public List<ReadGerenteDto> RecuperarGerente()
        {
            List<Gerente> gerentes = _context.Gerentes.ToList();
            return _mapper.Map<List<ReadGerenteDto>>(gerentes);
        }

        public ReadGerenteDto RecuperarGerenteId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente != null)
            {
                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
                return gerenteDto;
            }
            return null;
        }
        public Result ApagarGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null)
            {
                return Result.Fail("Gerente não encontrado.");
            }
            _context.Remove(gerente);
            _context.SaveChanges();
            return Result.Ok();

        }
    }
}
