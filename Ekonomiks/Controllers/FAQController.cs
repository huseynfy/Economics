using System.Linq;
using Ekonomiks.DAL;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Controllers
{
    public class FAQController : Controller
    {
        private readonly AppDbContext _db;
        public FAQController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AllViewModel vm = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1)
            };
            return View(vm);
        }
    }
}