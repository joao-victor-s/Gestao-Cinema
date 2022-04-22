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
    public class FilmesService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmesService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto AdicionaFilmes(CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto); //pega os dados do filmedto e passamos para o filme
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return _mapper.Map<ReadFilmeDto>(filme);
        }
        public List<ReadFilmeDto> RecuperarFilmes(int? classificacaoetaria = null)
        {
            List<Filme> filmes;
            if (classificacaoetaria == null)
            {
                filmes = _context.Filmes.ToList();
            }
            else
            {
                filmes = _context
                   .Filmes.Where(filmes => filmes.ClassificadoraEtaria <= classificacaoetaria).ToList();

            }

            if (filmes != null)
            {
                List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return readDto;
            }
            return null;
        }

        public ReadFilmeDto RecuperarFilmesId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme); //converter nosso readfilmedto para um filme
                
                return filmeDto;
            }
            return null;
        }

        public Result AtualizarFilmes(int id, UpdateFilmeDto filmedto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return Result.Fail("Filme não encontrado.");
            }
            _mapper.Map(filmedto, filme); 
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result ApagarFilmes(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return Result.Fail("Filme não encontrado.");
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
