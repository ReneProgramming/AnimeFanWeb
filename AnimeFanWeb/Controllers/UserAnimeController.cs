﻿using AnimeFanWeb.Data;
using AnimeFanWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeFanWeb.Controllers
{
    [Authorize] 
    public class UserAnimeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserAnimeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Display the user's watchlist
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var watchlist = await _context.UserAnime
                .Include(ua => ua.Anime)
                .Where(ua => ua.UserId == user.Id)
                .ToListAsync();

            return View(watchlist);
        }

        // Add an anime to the watchlist
        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(int animeId)
        {
            var user = await _userManager.GetUserAsync(User);

            var existingEntry = await _context.UserAnime
                .FirstOrDefaultAsync(ua => ua.UserId == user.Id && ua.AnimeListId == animeId);

            if (existingEntry == null)
            {
                var userAnime = new UserAnime
                {
                    UserId = user.Id,
                    AnimeListId = animeId,
                    Status = WatchStatus.ToWatch // Default status
                };

                _context.UserAnime.Add(userAnime);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // Update the status of an anime in the watchlist
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, WatchStatus status)
        {
            var userAnime = await _context.UserAnime.FindAsync(id);
            if (userAnime != null)
            {
                userAnime.Status = status;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // Remove an anime from the watchlist
        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(int id)
        {
            var userAnime = await _context.UserAnime.FindAsync(id);
            if (userAnime != null)
            {
                _context.UserAnime.Remove(userAnime);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
