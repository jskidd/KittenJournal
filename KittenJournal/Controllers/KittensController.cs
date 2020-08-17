using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KittenJournal.DAL;
using KittenJournal.Models;
using KittenJournal.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using KittenJournal.Models.Identity;
using Microsoft.AspNetCore.Authorization;

namespace KittenJournal
{
    public class KittensController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<KittenJournalUser> _userManager;

        public KittensController(AppDbContext context, UserManager<KittenJournalUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Kittens
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<KittenViewModel> vm = new List<KittenViewModel>();
            List<Kitten> kittens;

            if (User.IsInRole("Foster"))
            {
                KittenJournalUser user = await _userManager.GetUserAsync(User);
                kittens = await _context.Kittens.Where(k => k.FosterId == user.FosterId).ToListAsync();
            } else
            {
                kittens = await _context.Kittens.ToListAsync();
            }

            foreach (var kitten in kittens)
            {
                vm.Add(new KittenViewModel
                {
                    Kitten = kitten,
                    Foster = _context.Fosters.Find(kitten.FosterId)
                });
            }

            return View(vm.ToList());
        }

        // GET: Kittens/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitten = await _context.Kittens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kitten == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Administrator") || (await _userManager.GetUserAsync(User)).FosterId == kitten.FosterId)
            {
                return View(new KittenViewModel()
                {
                    Kitten = kitten,
                    Foster = _context.Fosters.Where(f => f.Id == kitten.FosterId).FirstOrDefault(),
                    Feedings = await _context.Feedings.Where(f => f.KittenId == kitten.Id).OrderByDescending(f => f.Timestamp).ToListAsync()
                }); ;
            }

            return Redirect("/Identity/Account/AccessDenied");
        }

        // GET: Kittens/Create
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Fosters = await _context.Fosters.ToListAsync();
            return View();
        }

        // POST: Kittens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,CurrentWeight,Sex,FosterId")] Kitten kitten)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kitten);
        }

        // GET: Kittens/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitten = await _context.Kittens.FindAsync(id);
            if (kitten == null)
            {
                return NotFound();
            }

            CreateEditKittenViewModel vm = new CreateEditKittenViewModel()
            {
                kitten = kitten,
                FostersList = _context.Fosters.ToList()
            };

            return View(vm);
        }

        // POST: Kittens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CurrentWeight,Sex,FosterId")] Kitten kitten)
        {
            if (id != kitten.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitten);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KittenExists(kitten.Id))
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
            return View(kitten);
        }

        // GET: Kittens/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitten = await _context.Kittens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kitten == null)
            {
                return NotFound();
            }

            return View(kitten);
        }

        // POST: Kittens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitten = await _context.Kittens.FindAsync(id);
            _context.Feedings.RemoveRange(_context.Feedings.Where(f => f.KittenId == id));
            _context.Kittens.Remove(kitten);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KittenExists(int id)
        {
            return _context.Kittens.Any(e => e.Id == id);
        }
    }
}
