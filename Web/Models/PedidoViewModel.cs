using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PedidoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Data pedido")]
        public DateTime DataPedido { get; set; }

        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; }

        [Display(Name = "R$ Desconto")]
        public double Desconto { get; set; }

        [Display(Name = "R$ Frete")]
        public double TaxaFrete { get; set; }

        [Display(Name = "R$ Total")]
        public double ValorTotal { get; set; }

        [Display(Name = "Restaurante")]
        public int RestauranteId { get; set; }

        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        public virtual RestauranteViewModel Restaurante { get; set; }

        public virtual ProdutoViewModel Produto { get; set; }
    }
}
