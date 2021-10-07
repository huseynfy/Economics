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
    public class ContactController : Controller
    {
        private readonly AppDbContext db;
        public ContactController(AppDbContext dbs)
        {
            db = dbs;
        }
        public IActionResult Index()
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(db.Contacts.FirstOrDefault(x=>x.Id==1));
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View(await db.Contacts.FindAsync(id));
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Contact contact)
        {
            ViewBag.MessageCount = db.Messages.Count();
            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}