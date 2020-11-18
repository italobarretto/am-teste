using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgenteMarketingTeste.Models.ViewModels
{
    public class VendaFormViewModel
    {
        public string Nome { get; set; }
        [Display(Name = "Data da Venda")]
        public DateTime DataVenda { get; set; }
        public decimal Valor { get; set; }
        public int VendaContatoId { get; set; }
        [Display(Name = "Nome do Produto")]
        public string NomeProduto { get; set; }
        public decimal Quantidade { get; set; }

        public VendaFormViewModel(string nome, DateTime dataVenda, decimal valor, int vendaContatoId)
        {
            Nome = nome;
            DataVenda = dataVenda;
            Valor = valor;
            VendaContatoId = vendaContatoId;
        }

        public VendaFormViewModel(string nome, DateTime dataVenda, decimal valor, string nomeProduto, decimal quantidade)
        {
            Nome = nome;
            DataVenda = dataVenda;
            Valor = valor;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
        }
    }
}
