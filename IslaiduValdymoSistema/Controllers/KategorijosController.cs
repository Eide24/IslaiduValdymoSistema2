using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IslaiduValdymoSistema.Data;
using IslaiduValdymoSistema.Models;
using Microsoft.AspNetCore.Authorization;
namespace IslaiduValdymoSistema.Controllers
{
    [Authorize]
    public class KategorijosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategorijosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()

        {
            var kategorijos = await _context.Kategorijos.ToListAsync();
            return View(kategorijos);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategorija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategorija);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var kategorija = await _context.Kategorijos.FindAsync(id);
            if (kategorija == null)
            {
                return NotFound();
            }
            return View(kategorija);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kategorija kategorija)
        {
            if (id != kategorija.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(kategorija);
                await _context.SaveChangesAsync();
            }
            return View(kategorija);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorija = await _context.Kategorijos
                .FirstOrDefaultAsync(k => k.Id == id);

            if (kategorija == null)
            {
                return NotFound();
            }

            return View(kategorija);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategorija = await _context.Kategorijos.FindAsync(id);

            if (kategorija != null)
            {
                _context.Kategorijos.Remove(kategorija);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}