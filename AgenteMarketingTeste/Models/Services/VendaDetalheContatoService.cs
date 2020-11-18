using AgenteMarketingTeste.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgenteMarketingTeste.Models.Services
{
    public class VendaDetalheContatoService
    {
        private readonly AgenteMarketingTesteContext _context;

        public VendaDetalheContatoService(AgenteMarketingTesteContext context)
        {
            _context = context;
        }
        public List<VendaContato> FindAllAsync()
        {
            return _context.VendaContato.OrderBy(x => x.VendaContatoId).ToList();
        }
    }
}
