using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgenteMarketingTeste.Models.ViewModels
{
    public class VendaContatoFormViewModel
    {
        public VendaContato VendaContato { get; set; }
        public int VendaDetalheContatoId { get; set; }
        public ICollection<Contato> Contatos { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public VendaDetalheContato VendaDetalheContato { get; set; }
    }
}
