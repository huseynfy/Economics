using System;
using System.Linq;
using Ekonomiks.DAL;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Controllers
{
    public class NewsController : Controller
    {
        private readonly AppDbContext _db;
        public NewsController (AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int Id)
        {
            AllViewModel vm = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                GetNews = _db.News.FirstOrDefault(news=>news.Id==Id),
                News=_db.News.OrderByDescending(x => x.DateTime).Where(x=>x.Id!=Id)
            };
            return View(vm);
        }
        public IActionResult More()
        {
            AllViewModel vm1 = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                News = _db.News.OrderByDescending(x => x.DateTime)
            };
            return View(vm1);
        }
    }
}