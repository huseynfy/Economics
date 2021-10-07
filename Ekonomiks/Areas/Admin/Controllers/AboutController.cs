using System.Linq;
using System.Threading.Tasks;
using Ekonomiks.DAL;
using Ekonomiks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext db;
        public AboutController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(db.Abouts.FirstOrDefault(x=>x.Id==1));
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(await db.Abouts.FindAsync(id));
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(About about)
        {
            ViewBag.MessageCount = db.Messages.Count();
            db.Entry(about).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}