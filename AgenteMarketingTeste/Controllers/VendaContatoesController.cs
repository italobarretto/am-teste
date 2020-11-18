using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgenteMarketingTeste.Models;
using AgenteMarketingTeste.Models.Data;
using AgenteMarketingTeste.Models.Services;
using AgenteMarketingTeste.Models.ViewModels;

namespace AgenteMarketingTeste.Controllers
{
    public class VendaContatoesController : Controller
    {
        private readonly AgenteMarketingTesteContext _context;
        private readonly VendaContatoService _vendaContatoService;

        public VendaContatoesController(AgenteMarketingTesteContext context, VendaContatoService vendaContatoService)
        {
            _context = context;
            _vendaContatoService = vendaContatoService;
        }

        // GET: VendaContatoes
        public IActionResult Index()
        {
            var contatos = _context.Contato.FromSql("spListarContatos").ToList();
            var vendaContatos = _context.VendaContato.FromSql("spListarVendas").ToList();
            var query = (from a in vendaContatos
                         join b in contatos
                        on a.ContatoId equals b.ContatoId
                        into temp
                        from b in temp.DefaultIfEmpty()
                        select new 
                        {
                            b.Nome,
                            a.DataVenda,
                            a.Valor,
                            a.VendaContatoId
                        }).OrderBy(x => x.DataVenda).ToList();
            
            List<VendaFormViewModel> viewModel = new List<VendaFormViewModel>();

            foreach(var item in query)
            {
                viewModel.Add(new VendaFormViewModel(item.Nome, item.DataVenda, item.Valor, item.VendaContatoId));
            }

            return View(viewModel);
        }

        // GET: VendaContatoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendaContato = await _context.VendaContato
                .FirstOrDefaultAsync(m => m.VendaContatoId == id);
            if (vendaContato == null)
            {
                return NotFound();
            }

            var contatos = _vendaContatoService.FindAllContatos();
            var produtos = _vendaContatoService.FindAllProdutos();
            var vendasDetalheContato = _context.VendaDetalheContato.FromSql("spListarDetalhesVendas").ToList();

            var vendaDetalhe = vendasDetalheContato.Where(v => v.VendaContatoId == vendaContato.VendaContatoId).FirstOrDefault();
            var nome = contatos.Where(c => c.ContatoId == vendaContato.ContatoId).Select(c => c.Nome).FirstOrDefault();
            var dataVenda = vendaContato.DataVenda;
            var valor = vendaContato.Valor;
            var nomeProduto = produtos.Where(p => p.ProdutoId == vendaDetalhe.ProdutoId).Select(p => p.NomeProduto).FirstOrDefault();
            var quantidade = vendasDetalheContato.Where(v => v.VendaContatoId == vendaContato.VendaContatoId).Select(v => v.Quantidade).FirstOrDefault();

            var viewModel = new VendaFormViewModel(nome, dataVenda, valor, nomeProduto, quantidade);

            return View(viewModel);
        }

        // GET: VendaContatoes/Create
        public IActionResult Create()
        {
            var contatos = _vendaContatoService.FindAllContatos();
            var produtos = _vendaContatoService.FindAllProdutos();
            var viewModel = new VendaContatoFormViewModel { Contatos = contatos, Produtos = produtos };
            return View(viewModel);
        }

        // POST: VendaContatoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("VendaContatoId,ContatoId,DataVenda,Valor")] VendaContato vendaContato,
            [Bind("VendaDetalheContatoId,VendaContatoId,ProdutoId,Quantidade,Valor")] VendaDetalheContato vendaDetalheContato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendaContato);
                _context.SaveChanges();
                vendaDetalheContato.VendaContatoId = _context.VendaContato.Last().VendaContatoId;
                vendaDetalheContato.Valor = vendaContato.Valor;
                _context.Add(vendaDetalheContato);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendaContato);
        }

        // GET: VendaContatoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendaContato = await _context.VendaContato.FindAsync(id);
            if (vendaContato == null)
            {
                return NotFound();
            }

            var contatos = _vendaContatoService.FindAllContatos();
            var produtos = _vendaContatoService.FindAllProdutos();
            var viewModel = new VendaContatoFormViewModel { Contatos = contatos, Produtos = produtos, VendaContato = vendaContato };

            return View(viewModel);
        }

        // POST: VendaContatoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendaContatoId,ContatoId,DataVenda,Valor")] VendaContato vendaContato,
            [Bind("VendaDetalheContatoId,VendaContatoId,ProdutoId,Quantidade,Valor")] VendaDetalheContato vendaDetalheContato)
        {
            if (id != vendaContato.VendaContatoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendaContato);
                    _context.SaveChanges();
                    vendaDetalheContato.VendaContatoId = _context.VendaContato.Where(e => e.VendaContatoId == vendaContato.VendaContatoId).Select(e => e.VendaContatoId).FirstOrDefault();
                    vendaDetalheContato.VendaDetalheContatoId = _context.VendaDetalheContato.FromSql("spListarDetalhesVendas").ToList().Where(e => e.VendaContatoId == vendaContato.VendaContatoId).Select(e => e.VendaDetalheContatoId).FirstOrDefault();
                    vendaDetalheContato.Valor = vendaContato.Valor;
                    _context.Update(vendaDetalheContato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaContatoExists(vendaContato.VendaContatoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendaContato);
        }

        // GET: VendaContatoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendaContato = await _context.VendaContato
                .FirstOrDefaultAsync(m => m.VendaContatoId == id);
            if (vendaContato == null)
            {
                return NotFound();
            }

            return View(vendaContato);
        }

        // POST: VendaContatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendaContato = await _context.VendaContato.FindAsync(id);
            _context.VendaContato.Remove(vendaContato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaContatoExists(int id)
        {
            return _context.VendaContato.Any(e => e.VendaContatoId == id);
        }
    }
}
