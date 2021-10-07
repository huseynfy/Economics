using System.Linq;
using System.Threading.Tasks;
using Ekonomiks.DAL;
using Ekonomiks.Models;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AllViewModel vm = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                Messages = _db.Messages
            };
            return View(vm);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Message message)
        {
            if (!ModelState.IsValid)
            {
                TempData["UnSuccess"] = "Yazı daxil edin!";
                AllViewModel vm = new AllViewModel()
                {
                    Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                    Messages = _db.Messages
                };
                return View(vm);
            }
            else
            {
                TempData["Success"] = "Məktubunuz göndərildi!";
                await _db.Messages.AddAsync(message);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
          
        }
    }
}