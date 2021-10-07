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
    public class ProjectController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment _env;
        public ProjectController(AppDbContext _db, IWebHostEnvironment env)
        {
            db = _db;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(db.Projects.OrderByDescending(x=>x.DateTime));
        }
        public async Task<IActionResult> Update(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Project project)
        {
            Project projects = await db.Projects.FindAsync(id);
            projects.Header = project.Header;
            projects.DateTime = project.DateTime;
            projects.Importance = project.Importance;
            projects.Goal = project.Goal;
            projects.Info = project.Info;
            if (project.Photo != null)
            {
                if (!project.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "This is not image");
                    return View();
                }
                if (project.Photo.LessThan(2))
                {
                    ModelState.AddModelError("Photo", "Image must be max 2mb");
                    return View();
                }
                Project project1 = await db.Projects.FindAsync(id);
                //FileHelper.DeleteImg(_env.WebRootPath, "img", project1.Image);
                string filename = await project.Photo.SavePhoto(_env.WebRootPath, "img");
                project1.Image = filename;
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                projects.Image = projects.Image;
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project work)
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
            await db.Projects.AddAsync(work);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}