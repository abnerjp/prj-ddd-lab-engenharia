using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class RestauranteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Cozinha")]
        public int CozinhaId { get; set; }

        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }

        public virtual CozinhaViewModel Cozinha { get; set; }

        public virtual CidadeViewModel Cidade { get; set; }

    }
}
