using APIFilmes.Data;
using APIFilmes.Data.Dtos;
using APIFilmes.Data.Dtos.Sessao;
using APIFilmes.Models;
using APIFilmes.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFilmes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService;
        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessaoDto sessaoDto)
        {
            ReadSessaoDto readto = _sessaoService.AdicionaSessao(sessaoDto);
            return CreatedAtAction(nameof(RecuperaSessaoId), new { Id = readto.Id }, readto);
        }

        [HttpGet]
        public IActionResult RecuperarSessao()
        {
            List<ReadSessaoDto> readto = _sessaoService.RecuperarSessao();
            if (readto != null)
                return Ok(readto);
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaSessaoId(int id)
        {
            ReadSessaoDto readto = _sessaoService.RecuperaSessaoId(id);
            if (readto != null)
                return Ok(readto);
            return NotFound();
        }
    }
}
