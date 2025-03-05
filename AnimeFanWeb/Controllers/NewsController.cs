using AnimeFanWeb.Data;
using AnimeFanWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeFanWeb.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var newsList = await _context.News.ToListAsync();

            var viewModel = newsList.Select(news => new NewsIndexViewModel
            {
                Id = news.Id,
                Title = news.Title,
                Summary = news.Summary,
                ImageFileName = news.ImageFileName,
                RelatedLinks = news.RelatedLinks,
                DatePublished = news.DatePublished 
            }).ToList();

            return View(viewModel);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View(new NewsCreateViewModel());
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (model.ImageFile != null)
                {
                    // Ensure directory exists
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "news");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder); 
                    }

                    fileName = $"{Guid.NewGuid()}_{model.ImageFile.FileName}";
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    // Save file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }
                }

                // Save to database
                var news = new News
                {
                    Title = model.Title,
                    Summary = model.Summary,
                    Content = model.Content,
                    RelatedLinks = model.RelatedLinks,
                    DatePublished = model.DatePublished,
                    ImageFileName = fileName 
                };

                _context.News.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var newsItem = await _context.News.FirstOrDefaultAsync(n => n.Id == id);

            if (newsItem == null)
            {
                return NotFound();
            }

            return View(newsItem);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var newsItem = await _context.News.FindAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            var model = new NewsEditViewModel
            {
                Id = newsItem.Id,
                Title = newsItem.Title,
                Summary = newsItem.Summary,
                Content = newsItem.Content,
                RelatedLinks = newsItem.RelatedLinks,
                DatePublished = newsItem.DatePublished,
                ImageFileName = newsItem.ImageFileName
            };

            return View(model);
        }

        // POST: News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewsEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var existingNews = await _context.News.FindAsync(id);
                if (existingNews == null)
                {
                    return NotFound();
                }

                // Update text fields
                existingNews.Title = model.Title;
                existingNews.Summary = model.Summary;
                existingNews.Content = model.Content;
                existingNews.RelatedLinks = model.RelatedLinks;
                existingNews.DatePublished = model.DatePublished;

                // image upload
                if (model.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "news");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate unique filename
                    string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.ImageFile.FileName)}";
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(existingNews.ImageFileName))
                    {
                        string oldFilePath = Path.Combine(uploadsFolder, existingNews.ImageFileName);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Save new image file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    // Update image filename in database
                    existingNews.ImageFileName = fileName;
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> NewsExists(int id)
        {
            return await _context.News.AnyAsync(e => e.Id == id);
        }
    }

}
