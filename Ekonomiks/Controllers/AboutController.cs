using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ekonomiks.DAL;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        public AboutController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AllViewModel vm = new AllViewModel()
            {
                Abouts = _db.Abouts.FirstOrDefault(about => about.Id == 1),
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1)
            };
            return View(vm);
        }
    }
}