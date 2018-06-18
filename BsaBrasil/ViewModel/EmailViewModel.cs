using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BsaBrasil.ViewModel
{
    public class EmailViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Assunto { get; set; }

        [Required]
        [MinLength(10)]
        public string Mensagem { get; set; }
    }
}
