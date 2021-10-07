using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ekonomiks.DAL;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AllViewModel vm = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                News=_db.News.OrderByDescending(x => x.DateTime),
                Interviews=_db.Interviews.OrderByDescending(x => x.DateTime),
                Actuals=_db.Actuals.OrderByDescending(x => x.DateTime),
                Projects=_db.Projects
            };
            return View(vm);
        }
    }
}