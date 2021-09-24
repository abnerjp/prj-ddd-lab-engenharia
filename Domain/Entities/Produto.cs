using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Produto : BaseEntity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public double Preco { get; set; }
    }
}
