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
    public class EnderecoController : ControllerBase
    {
        private EnderecoService _enderecoService;
        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }
        //utilizamos o context pq é ele que liga ao banco de dados
        [HttpPost] 
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            ReadEnderecoDto readto = _enderecoService.AdicionaEndereco(enderecoDto);
            
            return CreatedAtAction(nameof(RecuperarEnderecoId), new { Id = readto.Id }, readto);
        }

        [HttpGet]
        public IActionResult RecuperarEndereco()
        {
            List<ReadEnderecoDto> readto = _enderecoService.RecuperarEndereco();
            if(readto != null)
                return Ok(readto);
            return NotFound();
        }

        [HttpGet("{id}")] 
        public IActionResult RecuperarEnderecoId(int id)
        {
            ReadEnderecoDto readto = _enderecoService.RecuperarEnderecoId(id);
            if (readto != null)
                return Ok(readto);
            return NotFound();

        }

        [HttpPut("{id}")]
        public IActionResult AtualizarEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Result resultado = _enderecoService.AtualizarEndereco(id, enderecoDto);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult ApagarEndereco(int id)
        {
            Result resultado = _enderecoService.ApagarEndereco(id);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();

        }
    }
}
