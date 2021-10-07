using System.Linq;
using System.Threading.Tasks;
using Ekonomiks.DAL;
using Ekonomiks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {
        private readonly AppDbContext db;
        public MessageController (AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(db.Messages);
        }
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(await db.Messages.FindAsync(id));
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Message message)
        {
            ViewBag.MessageCount = db.Messages.Count();
            db.Messages.Remove(message);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}