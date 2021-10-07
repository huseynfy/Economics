using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ekonomiks.DAL;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _db;
        public SearchController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AllViewModel vm = new AllViewModel
            {
                News = _db.News.OrderByDescending(x=>x.DateTime),
                Contacts = _db.Contacts.FirstOrDefault(x => x.Id == 1)
            };
            return View(vm);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Index(string search)
        {
            ViewBag.Search = search;

            if (search==null)
            {
                ModelState.AddModelError("", "Yazı daxil edin!");
                AllViewModel vm = new AllViewModel
                {
                    Contacts = _db.Contacts.FirstOrDefault(x => x.Id == 1),
                    News = _db.News.OrderByDescending(x => x.DateTime)
                };
                return View(vm);
            }

            AllViewModel vm1 = new AllViewModel
            {
                News = _db.News.OrderByDescending(x=>x.DateTime).Where(x => x.Header.ToLower().Contains(search)),
                Interviews = _db.Interviews.OrderByDescending(x => x.DateTime).Where(x => x.Header.ToLower().Contains(search)),
                Contacts = _db.Contacts.FirstOrDefault(x => x.Id == 1)
            };
            return View(vm1);
        }
    }
}