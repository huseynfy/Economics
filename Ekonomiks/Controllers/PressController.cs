using System.Linq;
using Ekonomiks.DAL;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Controllers
{
    public class PressController : Controller
    {
        private readonly AppDbContext _db;
        public PressController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AllViewModel vm = new AllViewModel {
                 Contacts=_db.Contacts.FirstOrDefault(x=>x.Id==1),
                 News=_db.News.OrderByDescending(x=>x.DateTime)
            };
            return View(vm);
        }
        public IActionResult More(string categoryName,string heading)
        {
            AllViewModel vm = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                News = _db.News.OrderByDescending(x => x.DateTime).Where(x => x.Category == categoryName),
                Header=heading
            };
            return View(vm);
        }
    }
}