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
    public class VendaDetalheContatoesController : Controller
    {
        private readonly AgenteMarketingTesteContext _context;
        private readonly VendaDetalheContatoService _vendaDetalheContatoService;

        public VendaDetalheContatoesController(AgenteMarketingTesteContext context, VendaDetalheContatoService vendaDetalheContatoService)
        {
            _context = context;
            _vendaDetalheContatoService = vendaDetalheContatoService;
        }

        // GET: VendaDetalheContatoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendaDetalheContato.FromSql("spListarDetalhesVendas").ToListAsync());
        }

        // GET: VendaDetalheContatoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendaDetalheContato = await _context.VendaDetalheContato
                .FirstOrDefaultAsync(m => m.VendaDetalheContatoId == id);
            if (vendaDetalheContato == null)
            {
                return NotFound();
            }

            return View(vendaDetalheContato);
        }

        // GET: VendaDetalheContatoes/Create
        public IActionResult Create()
        {
            var vendaContatos = _context.VendaContato.FromSql("spListarVendas").ToList();
            var viewModel = new VendaDetalheContatoFormViewModel { VendaContatos = vendaContatos };
            return View(viewModel);
        }

        // POST: VendaDetalheContatoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendaDetalheContatoId,VendaContatoId,ProdutoId,Quantidade,Valor")] VendaDetalheContato vendaDetalheContato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendaDetalheContato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendaDetalheContato);
        }

        // GET: VendaDetalheContatoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendaDetalheContato = await _context.VendaDetalheContato.FindAsync(id);
            if (vendaDetalheContato == null)
            {
                return NotFound();
            }
            return View(vendaDetalheContato);
        }

        // POST: VendaDetalheContatoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendaDetalheContatoId,VendaContatoId,ProdutoId,Quantidade,Valor")] VendaDetalheContato vendaDetalheContato)
        {
            if (id != vendaDetalheContato.VendaDetalheContatoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendaDetalheContato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaDetalheContatoExists(vendaDetalheContato.VendaDetalheContatoId))
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
            return View(vendaDetalheContato);
        }

        // GET: VendaDetalheContatoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendaDetalheContato = await _context.VendaDetalheContato
                .FirstOrDefaultAsync(m => m.VendaDetalheContatoId == id);
            if (vendaDetalheContato == null)
            {
                return NotFound();
            }

            return View(vendaDetalheContato);
        }

        // POST: VendaDetalheContatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendaDetalheContato = await _context.VendaDetalheContato.FindAsync(id);
            _context.VendaDetalheContato.Remove(vendaDetalheContato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaDetalheContatoExists(int id)
        {
            return _context.VendaDetalheContato.Any(e => e.VendaDetalheContatoId == id);
        }
    }
}
