using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenteMarketingTeste.Models
{
    public class Contato
    {
        public int ContatoId { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public List<VendaContato> ListaVendas { get; set; }

    }
}
