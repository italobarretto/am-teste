using AgenteMarketingTeste.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgenteMarketingTeste.Models.Services
{
    public class VendaContatoService
    {
        private readonly AgenteMarketingTesteContext _context;

        public VendaContatoService(AgenteMarketingTesteContext context)
        {
            _context = context;
        }
        public List<Contato> FindAllContatos()
        {
            return _context.Contato.OrderBy(x => x.Nome).ToList();
        }
        public List<Produto> FindAllProdutos()
        {
            return _context.Produto.OrderBy(x => x.NomeProduto).ToList();
        }
    }
}
