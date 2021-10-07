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
    public class ActualController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment _env;
        public ActualController(AppDbContext _db,IWebHostEnvironment env)
        {
            db = _db;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(db.Actuals.OrderByDescending(x=>x.DateTime));
        }
        public async Task<IActionResult> Update(int id)
        {
            Actual actual = await db.Actuals.FindAsync(id);
            if (actual == null) return NotFound();
            return View(actual);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Actual actual)
        {
            Actual actuals = await db.Actuals.FindAsync(id);
            actuals.Header = actual.Header;
            actuals.Body = actual.Body;
            actuals.DateTime = actual.DateTime;
            actuals.ReadTime = actual.ReadTime;
            actuals.Category = actual.Category;
            if (actual.Photo != null)
            {
                if (!actual.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "This is not image");
                    return View();
                }
                if (actual.Photo.LessThan(2))
                {
                    ModelState.AddModelError("Photo", "Image must be max 2mb");
                    return View();
                }
                Actual actuals1 = await db.Actuals.FindAsync(id);
                //FileHelper.DeleteImg(_env.WebRootPath, "img", actuals1.Image);
                string filename = await actual.Photo.SavePhoto(_env.WebRootPath, "img");
                actuals1.Image = filename;
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                actuals.Image = actuals.Image;
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Actual work)
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
            await db.Actuals.AddAsync(work);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Actual actual = await db.Actuals.FindAsync(id);
            if (actual == null) return NotFound();
            return View(actual);
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteWork(int id)
        {
            Actual work = await db.Actuals.FindAsync(id);
            if (work == null) return NotFound();
            FileHelper.DeleteImg(_env.WebRootPath, "img", work.Image);
            db.Actuals.Remove(work);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}