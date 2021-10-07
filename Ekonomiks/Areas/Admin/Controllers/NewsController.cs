using System.Linq;
using System.Threading.Tasks;
using Ekonomiks.DAL;
using Ekonomiks.Models;
using FrontToBack.Extentions;
using FrontToBack.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment _env;
        public NewsController(AppDbContext _db, IWebHostEnvironment env)
        {
            db = _db;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(db.News.OrderByDescending(x => x.DateTime));
        }
        public async Task<IActionResult> Update(int id)
        {
            News news = await db.News.FindAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, News news)
        {
            News newss = await db.News.FindAsync(id);
            newss.Header = news.Header;
            newss.Body = news.Body;
            newss.DateTime = news.DateTime;
            newss.ReadTime = news.ReadTime;
            newss.Category = news.Category;
            if (news.Photo != null)
            {
                if (!news.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "This is not image");
                    return View();
                }
                if (news.Photo.LessThan(2))
                {
                    ModelState.AddModelError("Photo", "Image must be max 2mb");
                    return View();
                }
                News news1 = await db.News.FindAsync(id);
                //FileHelper.DeleteImg(_env.WebRootPath, "img", news1.Image);
                string filename = await news.Photo.SavePhoto(_env.WebRootPath, "img");
                news1.Image = filename;
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                newss.Image = newss.Image;
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News work)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!work.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "This is not image");
                return View();
            }
            if (work.Photo.LessThan(2))
            {
                ModelState.AddModelError("Photo", "Image must be max 2mb");
                return View();
            }
            string filename = await work.Photo.SavePhoto(_env.WebRootPath, "img");
            work.Image = filename;
            await db.News.AddAsync(work);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            News news = await db.News.FindAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteWork(int id)
        {
            News work = await db.News.FindAsync(id);
            if (work == null) return NotFound();
            FileHelper.DeleteImg(_env.WebRootPath, "img", work.Image);
            db.News.Remove(work);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}