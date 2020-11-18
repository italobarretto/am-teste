using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenteMarketingTeste.Models
{
    public class VendaDetalheContato
    {
        public int VendaDetalheContatoId { get; set; }
        public int VendaContatoId { get; set; }
        public int ProdutoId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        //public List<Produto> ListaProdutos { get; set; }

    }
}
