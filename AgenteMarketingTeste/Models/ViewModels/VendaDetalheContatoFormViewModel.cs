using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgenteMarketingTeste.Models.ViewModels
{
    public class VendaDetalheContatoFormViewModel
    {
        public VendaDetalheContato VendaDetalheContato { get; set; }
        public ICollection<VendaContato> VendaContatos { get; set; }
    }
}
