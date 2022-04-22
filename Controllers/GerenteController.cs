using APIFilmes.Data;
using APIFilmes.Data.Dtos;
using APIFilmes.Models;
using APIFilmes.Services;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        GerenteService _gerenteService;
        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpPost]
        public IActionResult AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            ReadGerenteDto readto = _gerenteService.AdicionaGerente(gerenteDto); 
            return CreatedAtAction(nameof(RecuperarGerenteId), new { Id = readto.Id }, readto);
        }
        [HttpGet]
        public IActionResult RecuperarGerente()
        {
            List<ReadGerenteDto> readto = _gerenteService.RecuperarGerente();
            if (readto != null)
                return Ok(readto);
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarGerenteId(int id)
        {
            ReadGerenteDto readto = _gerenteService.RecuperarGerenteId(id);
            if (readto != null)
                return Ok(readto);
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult ApagarGerente(int id)
        {
            Result resultado = _gerenteService.ApagarGerente(id);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();
        }
    }
}
