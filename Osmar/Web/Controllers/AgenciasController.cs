using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.DBModels;
using Web.Data;

namespace Web.Controllers
{
    public class AgenciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agencias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agencia.ToListAsync());
        }

        // GET: Agencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencia = await _context.Agencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agencia == null)
            {
                return NotFound();
            }

            return View(agencia);
        }

        // GET: Agencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone")] Agencia agencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agencia);
        }

        // GET: Agencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencia = await _context.Agencia.FindAsync(id);
            if (agencia == null)
            {
                return NotFound();
            }
            return View(agencia);
        }

        // POST: Agencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone")] Agencia agencia)
        {
            if (id != agencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgenciaExists(agencia.Id))
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
            return View(agencia);
        }

        // GET: Agencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencia = await _context.Agencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agencia == null)
            {
                return NotFound();
            }

            return View(agencia);
        }

        // POST: Agencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agencia = await _context.Agencia.FindAsync(id);
            _context.Agencia.Remove(agencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgenciaExists(int id)
        {
            return _context.Agencia.Any(e => e.Id == id);
        }
    }
}
