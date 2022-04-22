using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIFilmes.Models;
using APIFilmes.Data;
using APIFilmes.Data.Dtos;
using AutoMapper;
using APIFilmes.Services;
using FluentResults;

namespace APIFilmes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        FilmesService _filmesService;
        
        public FilmesController(FilmesService filmesService)
        {
            _filmesService = filmesService;
        }
        //utilizamos o context pq é ele que liga ao banco de dados
        [HttpPost]
        public IActionResult AdicionaFilmes([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto readto = _filmesService.AdicionaFilmes(filmeDto);
            return CreatedAtAction(nameof(RecuperarFilmesId), new { Id = readto.Id }, readto);
        }

        [HttpGet]
        public IActionResult RecuperarFilmes( [FromQuery] int? classificacaoetaria = null)
        {
            List<ReadFilmeDto> readto = _filmesService.RecuperarFilmes(classificacaoetaria);
            if (readto != null)
                return Ok(readto);
            return NotFound();
        }

        [HttpGet("{id}")] //parametro recebido
        public IActionResult RecuperarFilmesId(int id)
        {
            ReadFilmeDto readto = _filmesService.RecuperarFilmesId(id);
            if (readto != null)
                return Ok(readto);
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilmes(int id, [FromBody] UpdateFilmeDto filmedto)
        {
            Result resultado = _filmesService.AtualizarFilmes(id, filmedto);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult ApagarFilmes(int id)
        {
            Result resultado = _filmesService.ApagarFilmes(id);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();
        }
    }
}
