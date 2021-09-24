using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Restaurante : BaseEntity 
    {
        public string Nome { get; set; }

        public string Cep { get; set; }

        public string Bairro { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public int CozinhaId { get; set; }

        public int CidadeId { get; set; }

        public virtual Cozinha Cozinha { get; set; }

        public virtual Cidade Cidade { get; set; }
    }
}
