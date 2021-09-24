using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pedido : BaseEntity
    {
        public DateTime DataPedido { get; set; }

        public double Quantidade{ get; set; }

        public double Desconto { get; set; }

        public double TaxaFrete { get; set; }

        public double ValorTotal { get; set; }

        public int RestauranteId { get; set; }

        public int ProdutoId { get; set; }

        public virtual Restaurante Restaurante { get; set; }

        public virtual Produto Produto { get; set; }
    }
}
