using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AgenteMarketingTeste.Models
{
    public class VendaContato
    {
        public int VendaContatoId { get; set; }
        public int ContatoId { get; set; }
        [Display(Name = "Data da Venda")]
        public DateTime DataVenda { get; set; }
        public decimal Valor { get; set; }
        public List<VendaDetalheContato> ListaVendaDetalheContatos { get; set; }

    }
}
