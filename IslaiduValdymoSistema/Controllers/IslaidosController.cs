using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IslaiduValdymoSistema.Data;
using IslaiduValdymoSistema.Models;
using Microsoft.AspNetCore.Authorization;

namespace IslaiduValdymoSistema.Controllers
{
 [Authorize] 
    public class IslaidosController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public IslaidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(int? kategorijaId, DateTime? dataNuo, DateTime? dataIki)
        {
            var islaidos = _context.Islaidos
                .Include(i => i.Kategorija)
                .AsQueryable();

            
            if (kategorijaId != null)
            {
                islaidos = islaidos.Where(i => i.KategorijaId == kategorijaId);
            }

            if (dataNuo != null)
            {
                islaidos = islaidos.Where(i => i.Data >= dataNuo);
            }

            if (dataIki != null)
            {
                islaidos = islaidos.Where(i => i.Data <= dataIki);
            }

            var rezultatas = await islaidos.ToListAsync();

           
            ViewBag.BendraSuma = rezultatas.Sum(i => i.Suma).ToString("0.00");
            ViewBag.KategorijaId = new SelectList(_context.Kategorijos, "Id", "Pavadinimas", kategorijaId);
            ViewBag.DataNuo = dataNuo?.ToString("yyyy-MM-dd");
            ViewBag.DataIki = dataIki?.ToString("yyyy-MM-dd");
            ViewBag.SelectedKategorijaId = kategorijaId;

            return View(rezultatas);
        }

        
        public IActionResult Create()
        {
            ViewData["KategorijaId"] = new SelectList(
                _context.Kategorijos,
                "Id",
                "Pavadinimas"
            );

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Islaida islaida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(islaida);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["KategorijaId"] = new SelectList(
                _context.Kategorijos,
                "Id",
                "Pavadinimas",
                islaida.KategorijaId
            );

            return View(islaida);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islaida = await _context.Islaidos.FindAsync(id);
            if (islaida == null)
            {
                return NotFound();
            }

            ViewData["KategorijaId"] = new SelectList(_context.Kategorijos, "Id", "Pavadinimas", islaida.KategorijaId);
            return View(islaida);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Islaida islaida)
        {
            if (id != islaida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(islaida);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IslaidaExists(islaida.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["KategorijaId"] = new SelectList(_context.Kategorijos, "Id", "Pavadinimas", islaida.KategorijaId);
            return View(islaida);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islaida = await _context.Islaidos
                .Include(i => i.Kategorija)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (islaida == null)
            {
                return NotFound();
            }

            return View(islaida);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var islaida = await _context.Islaidos.FindAsync(id);
            if (islaida != null)
            {
                _context.Islaidos.Remove(islaida);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool IslaidaExists(int id)
        {
            return _context.Islaidos.Any(e => e.Id == id);
        }
    }
}