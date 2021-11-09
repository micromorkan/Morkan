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

            var agencia = await _context.Agencia.FirstOrDefaultAsync(m => m.Id == id);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,Email")] Agencia agencia)
        {
            if (string.IsNullOrWhiteSpace(agencia.Nome))
            {
                ModelState.AddModelError("Nome", "O campo Nome é obrigatório!");
            }
            else 
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone,Email")] Agencia agencia)
        {
            if (id != agencia.Id)
            {
                return NotFound();
            }
            else if (string.IsNullOrWhiteSpace(agencia.Nome))
            {
                ModelState.AddModelError("Nome", "O campo Nome é obrigatório!");
            }
            else if (AgenciaNameExists(agencia.Id, agencia.Nome))
            {
                ModelState.AddModelError("Nome", "O Nome informado já está cadastrado!");
            }
            else
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agencia = await _context.Agencia.FindAsync(id);
            var servicos = await _context.Servico.Where(x => x.AgenciaId == id).ToListAsync();

            if (servicos.Count() > 0)
            {
                TempData["BUSINESSERROR"] = "Não é possível excluir uma Agência que está em uso!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _context.Agencia.Remove(agencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool AgenciaExists(int id)
        {
            return _context.Agencia.Any(e => e.Id == id);
        }

        private bool AgenciaNameExists(int id, string nome)
        {
            return _context.Agencia.Any(e => e.Id != id && e.Nome.ToUpper() == nome.ToUpper());
        }
    }
}
