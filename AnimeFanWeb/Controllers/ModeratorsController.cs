﻿using System;
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
        private readonly ApplicationDbContext _context;

        public ModeratorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Moderators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Moderators.ToListAsync());
        }

        // GET: Moderators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moderator = await _context.Moderators
                .FirstOrDefaultAsync(m => m.Id == id);
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
                _context.Add(moderator);
                await _context.SaveChangesAsync();
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

            var moderator = await _context.Moderators.FindAsync(id);
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
                    _context.Update(moderator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeratorExists(moderator.Id))
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

            var moderator = await _context.Moderators
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var moderator = await _context.Moderators.FindAsync(id);

            // Change all related events to a null moderator
            using DbConnection con = _context.Database.GetDbConnection();
            await con.OpenAsync();
            using DbCommand query = con.CreateCommand();
            query.CommandText = "UPDATE Events SET ModeratorId = null WHERE ModeratorId = " + moderator.Id;
            int rowsAffected = await query.ExecuteNonQueryAsync();
            TempData["Message"] = $"{moderator.FullName} was removed from {rowsAffected} events";

            // Remove moderator
            if (moderator != null)
            {
                _context.Moderators.Remove(moderator);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeratorExists(int id)
        {
            return _context.Moderators.Any(e => e.Id == id);
        }
    }
}
