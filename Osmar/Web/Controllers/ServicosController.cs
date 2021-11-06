using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.DBModels;
using Web.Data;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Web.Business;
using Web.Business.Interface;
using Wkhtmltopdf.NetCore;
using HiQPdf;
using System.IO;

namespace Web.Controllers
{
    [Authorize]
    public class ServicosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGeneratePdf _generatePdf;
        private readonly IRazorPartialToStringRenderer _renderer;
        public ServicosController(IGeneratePdf reportService, ApplicationDbContext context, IRazorPartialToStringRenderer renderer)
        {
            _context = context;
            _generatePdf = reportService;
            _renderer = renderer;
        }

        [HttpGet]
        public async Task<IActionResult> GerarRecibo(int? serviceId)
        {
            return await _generatePdf.GetPdf("Views/Servicos/pdf.cshtml", "Hello World");
        }

        public async Task<IActionResult> Index()
        {           
            var applicationDbContext = _context.Servico.Include(s => s.Cliente);
            return View(await applicationDbContext.Where(x => x.DataServico.Date >= DateTime.Now.Date).OrderBy(o => o.DataServico).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["AgenciaId"] = new SelectList(_context.Agencia, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,AgenciaId,TransferIN,HorarioVoo,NumeroVoo,Companhia,DataVoo,Saida,QtdPassageiros,Veiculo,Observacao,Valor,DataServico")] Servico servico)
        {
            if (servico.DataServico.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("DataServico", "A Data do Serviço não pode ser inferior a data atual!");
            }
            else if (servico.DataVoo.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("DataVoo", "A Data do Vôo não pode ser inferior a data atual!");
            }
            else
            {
                servico.DataCadastro = DateTime.Now.Date;
                _context.Add(servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Cpf", servico.ClienteId);
            return View(servico);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Cpf", servico.ClienteId);
            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,TransferIN,HorarioVoo,NumeroVoo,Companhia,DataVoo,Saida,QtdPassageiros,Veiculo,Observacao,Valor,DataServico,DataCadastro")] Servico servico)
        {
            if (id != servico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(servico.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Cpf", servico.ClienteId);
            return View(servico);
        }

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servico = await _context.Servico.FindAsync(id);
            _context.Servico.Remove(servico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Pdf(int? id)
        {
            if (id.HasValue)
            {
                Servico servico = await _context.Servico.FindAsync(id);
                servico.Agencia = await _context.Agencia.FindAsync(servico.AgenciaId);
                servico.Cliente = await _context.Cliente.FindAsync(servico.ClienteId);

                HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
                htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

                string htmlToConvert = await _renderer.RenderPartialToStringAsync("Servicos/Pdf", servico);

                byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, $"{ this.Request.Scheme}://{this.Request.Host}");

                Response.Headers.Add("Content-Disposition", "inline; filename=document.pdf");
                Response.ContentType = "application/pdf";
                Response.ContentLength = pdfBuffer.Length;

                return new FileContentResult(pdfBuffer, "application/pdf");
            }
            else
            {
                return new FileContentResult(new byte[0], "application/pdf");
            }
        }

        private bool ServicoExists(int id)
        {
            return _context.Servico.Any(e => e.Id == id);
        }
    }
}
