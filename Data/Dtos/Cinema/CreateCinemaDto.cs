using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIFilmes.Data.Dtos
{
    public class CreateCinemaDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [Required]
        public int EnderecoId { get; set; }
        public int GerenteId { get; set; }
        
    }
}
