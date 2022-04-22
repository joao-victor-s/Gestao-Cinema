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
    public class CinemaService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto); //pega os dados do cinemadto e passamos para o cinema
            _context.Cinema.Add(cinema);
            _context.SaveChanges();
            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public List<ReadCinemaDto> RecuperarCinema(string nomefilme)
        {
            List<Cinema> cinemas = _context.Cinema.ToList();
            if (cinemas == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nomefilme))
            {
                IEnumerable<Cinema> query = from cinema in cinemas
                                            where cinema.Sessoes.Any(sessao =>
                                            sessao.Filme.Titulo == nomefilme)
                                            select cinema;
                cinemas = query.ToList();
            }
            return _mapper.Map<List<ReadCinemaDto>>(cinemas);
        }

        public ReadCinemaDto RecuperarCinemaId(int id)
        {
            Cinema cinema = _context.Cinema.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema); //converter nosso readcinemadto para um cinema
                return cinemaDto;
            }
            return null;
        }
        public Result AtualizarCinema(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinema.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado.");
            }
            _mapper.Map(cinemaDto, cinema); //sobrescrever
            _context.SaveChanges();
            return Result.Ok();
        }
        public Result ApagarCinema(int id)
        {
            Cinema cinema = _context.Cinema.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado.");
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
