﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimeFanWeb.Data;
using AnimeFanWeb.Models;

namespace AnimeFanWeb.Controllers
{
    public class AnimeListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimeListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AnimeLists
        public async Task<IActionResult> Index()
        {
            List<AnimeListIndexViewModel> listData = await (from L in _context.AnimeList
                                                            orderby L.Title
                                                            select new AnimeListIndexViewModel
                                                            {
                                                                AnimeListId = L.Id,
                                                                AnimeTitle = L.Title,
                                                                AnimeType = L.Type,
                                                                AnimeGenre = L.Genre,
                                                                AnimeStartDate = L.StartDate,
                                                                AnimeEndDate = L.EndDate,
                                                            }).ToListAsync();
                                                               
            return View(listData);                                               
        }

        // GET: AnimeLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animeList = await _context.AnimeList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animeList == null)
            {
                return NotFound();
            }

            return View(animeList);
        }

        // GET: AnimeLists/Create
        public IActionResult Create()
        {
            AnimeListCreateViewModel viewModel = new();
            return View(viewModel);
        }

        // POST: AnimeLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimeListCreateViewModel @list)
        {
            if (ModelState.IsValid)
            {
                AnimeList newAnime = new()
                {
                    Title = @list.Title,
                    Type = @list.Type,
                    Genre = @list.Genre,
                    Description = @list.Description,
                    StartDate = @list.StartDate,
                    EndDate = @list.EndDate,
                };
                _context.Add(newAnime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@list);
        }

        // GET: AnimeLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animeList = await _context.AnimeList.FindAsync(id);
            if (animeList == null)
            {
                return NotFound();
            }
            return View(animeList);
        }

        // POST: AnimeLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Type,Genre,StartDate,EndDate")] AnimeList animeList)
        {
            if (id != animeList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animeList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeListExists(animeList.Id))
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
            return View(animeList);
        }

        // GET: AnimeLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animeList = await _context.AnimeList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animeList == null)
            {
                return NotFound();
            }

            return View(animeList);
        }

        // POST: AnimeLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animeList = await _context.AnimeList.FindAsync(id);
            if (animeList != null)
            {
                _context.AnimeList.Remove(animeList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeListExists(int id)
        {
            return _context.AnimeList.Any(e => e.Id == id);
        }
    }
}
