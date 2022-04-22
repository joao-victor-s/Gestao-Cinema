using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIFilmes.Models;
using APIFilmes.Data;
using AutoMapper;
using APIFilmes.Data.Dtos;
using APIFilmes.Services;
using FluentResults;

namespace APIFilmes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private CinemaService _cinemaService;
        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            ReadCinemaDto readto = _cinemaService.AdicionaCinema(cinemaDto);
            
            return CreatedAtAction(nameof(RecuperarCinemaId), new { Id = readto.Id }, readto);
        }

        [HttpGet]
        public IActionResult RecuperarCinema([FromQuery] string nomefilme)
        {
            List<ReadCinemaDto> readto = _cinemaService.RecuperarCinema(nomefilme);
            if (readto != null) 
                return Ok(readto);
            return NotFound();
        }

        [HttpGet("{id}")] 
        public IActionResult RecuperarCinemaId(int id)
        {
            ReadCinemaDto readto = _cinemaService.RecuperarCinemaId(id);
            if (readto != null)
                return Ok(readto);
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Result resultado = _cinemaService.AtualizarCinema(id, cinemaDto);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult ApagarCinema(int id)
        {
            Result resultado = _cinemaService.ApagarCinema(id);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();

        }
    }
}
