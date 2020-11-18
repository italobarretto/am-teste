using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgenteMarketingTeste.Models;

namespace AgenteMarketingTeste.Models.Data
{
    public class AgenteMarketingTesteContext : DbContext
    {
        public AgenteMarketingTesteContext(DbContextOptions<AgenteMarketingTesteContext> options)
            : base(options)
        {

        }

        public DbSet<Contato> Contato { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<VendaContato> VendaContato { get; set; }
        public DbSet<VendaDetalheContato> VendaDetalheContato { get; set; }
    }
}