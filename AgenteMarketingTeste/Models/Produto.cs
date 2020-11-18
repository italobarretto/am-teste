using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenteMarketingTeste.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        [Display(Name = "Nome do Produto")]
        public string NomeProduto { get; set; }
    }
}
