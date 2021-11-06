using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.DBModels;
using Web.Data;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Cliente.OrderBy(o => o.Nome).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cpf,Email,Telefone1,Telefone2")] Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                ModelState.AddModelError("Nome", "O campo Nome é obrigatório!");
            }
            else if (string.IsNullOrWhiteSpace(cliente.Cpf))
            {
                ModelState.AddModelError("Cpf", "O campo Cpf é obrigatório!");
            }
            else if (ClienteCpfExists(cliente.Cpf))
            {
                ModelState.AddModelError("Cpf", "O Cpf informado já está em uso!");
            }
            else if (string.IsNullOrWhiteSpace(cliente.Telefone1))
            {
                ModelState.AddModelError("Telefone1", "O campo Telefone1 é obrigatório!");
            }
            else
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cpf,Email,Telefone1,Telefone2")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                ModelState.AddModelError("Nome", "O campo Nome é obrigatório!");
            }
            else if (string.IsNullOrWhiteSpace(cliente.Cpf))
            {
                ModelState.AddModelError("Cpf", "O campo Cpf é obrigatório!");
            }
            else if (ClienteCpfInUse(cliente.Id, cliente.Cpf))
            {
                ModelState.AddModelError("Cpf", "O Cpf informado já está em uso!");
            }
            else if (string.IsNullOrWhiteSpace(cliente.Telefone1))
            {
                ModelState.AddModelError("Telefone1", "O campo Telefone1 é obrigatório!");
            }
            else
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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

            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }

        private bool ClienteCpfInUse(int id, string cpf)
        {
            return _context.Cliente.Any(e => e.Id != id && e.Cpf == cpf);
        }

        private bool ClienteCpfExists(string cpf)
        {
            return _context.Cliente.Any(e => e.Cpf == cpf);
        }
    }
}
