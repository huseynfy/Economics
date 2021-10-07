using System;
using System.Linq;
using Ekonomiks.DAL;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Controllers
{
    public class ActualController : Controller
    {
        private readonly AppDbContext _db;
        public ActualController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int Id)
        {
            AllViewModel vm = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                GetActual = _db.Actuals.FirstOrDefault(actual => actual.Id == Id),
                Actuals = _db.Actuals.OrderByDescending(x => x.DateTime).Where(x => x.Id != Id)
            };
            return View(vm);
        }
        public IActionResult More()
        {
            AllViewModel vm1 = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                Actuals = _db.Actuals.OrderByDescending(x => x.DateTime)
            };
            return View(vm1);
        }
    }
}