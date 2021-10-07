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
    public class InterviewController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment _env;
        public InterviewController(AppDbContext _db, IWebHostEnvironment env)
        {
            db = _db;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(db.Interviews.OrderByDescending(x => x.DateTime));
        }
        public async Task<IActionResult> Update(int id)
        {
            Interview interview = await db.Interviews.FindAsync(id);
            if (interview == null) return NotFound();
            return View(interview);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Interview interview)
        {
            Interview interviews = await db.Interviews.FindAsync(id);
            interviews.Header = interview.Header;
            interviews.Body = interview.Body;
            interviews.DateTime = interview.DateTime;
            interviews.ReadTime = interview.ReadTime;
            interviews.Category = interview.Category;
            if (interview.Photo != null)
            {
                if (!interview.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "This is not image");
                    return View();
                }
                if (interview.Photo.LessThan(2))
                {
                    ModelState.AddModelError("Photo", "Image must be max 2mb");
                    return View();
                }
                Interview interviews1 = await db.Interviews.FindAsync(id);
                //FileHelper.DeleteImg(_env.WebRootPath, "img", interviews1.Image);
                string filename = await interview.Photo.SavePhoto(_env.WebRootPath, "img");
                interviews1.Image = filename;
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                interviews.Image = interviews.Image;
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Interview work)
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
            await db.Interviews.AddAsync(work);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Interview interview = await db.Interviews.FindAsync(id);
            if (interview == null) return NotFound();
            return View(interview);
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteWork(int id)
        {
            Interview work = await db.Interviews.FindAsync(id);
            if (work == null) return NotFound();
            FileHelper.DeleteImg(_env.WebRootPath, "img", work.Image);
            db.Interviews.Remove(work);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}