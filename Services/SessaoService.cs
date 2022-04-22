using APIFilmes.Data;
using APIFilmes.Data.Dtos;
using APIFilmes.Data.Dtos.Sessao;
using APIFilmes.Models;
using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFilmes.Services
{
    public class SessaoService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public SessaoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ReadSessaoDto AdicionaSessao(CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return _mapper.Map<ReadSessaoDto>(sessao);
        }

        public List<ReadSessaoDto> RecuperarSessao()
        {
            List<Sessao> sessoes = _context.Sessoes.ToList();
            return _mapper.Map<List<ReadSessaoDto>>(sessoes);
        }

        public ReadSessaoDto RecuperaSessaoId(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao != null)
            {
                ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
                return sessaoDto;
            }
            return null;
        }
    }
}
