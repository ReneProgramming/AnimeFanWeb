using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimeFanWeb.Data;
using AnimeFanWeb.Models;
using System.Data.Common;

namespace AnimeFanWeb.Controllers
{
    public class ModeratorsController : Controller
    {      
        private readonly IModeratorRepository _moderatorRepo;

        public ModeratorsController(IModeratorRepository moderatorRepo)
        {
            _moderatorRepo = moderatorRepo;
        }

        // GET: Moderators
        public async Task<IActionResult> Index()
        {
            return View(await _moderatorRepo.GetAllModerators());
        }

        // GET: Moderators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moderator = await _moderatorRepo.GetModerator(id.Value);
            if (moderator == null)
            {
                return NotFound();
            }

            return View(moderator);
        }

        // GET: Moderators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moderators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName")] Moderator moderator)
        {
            if (ModelState.IsValid)
            {
                await _moderatorRepo.SaveModerator(moderator);
                return RedirectToAction(nameof(Index));
            }
            return View(moderator);
        }

        // GET: Moderators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moderator = await _moderatorRepo.GetModerator(id.Value);
            if (moderator == null)
            {
                return NotFound();
            }
            return View(moderator);
        }

        // POST: Moderators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName")] Moderator moderator)
        {
            if (id != moderator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _moderatorRepo.UpdateModerator(moderator);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ModeratorExists(moderator.Id))
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
            return View(moderator);
        }

        // GET: Moderators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moderator = await _moderatorRepo.GetModerator(id.Value);
            if (moderator == null)
            {
                return NotFound();
            }

            return View(moderator);
        }

        // POST: Moderators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moderator = await _moderatorRepo.GetModerator(id);
           
            TempData["Message"] = $"{moderator.FullName} was removed from any related events";

            // Remove moderator
            
            await _moderatorRepo.DeleteModerator(moderator.Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ModeratorExists(int id)
        {
            return await _moderatorRepo.GetModerator(id) != null;
        }
    }
}
