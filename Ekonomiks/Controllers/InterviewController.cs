using System;
using System.Linq;
using Ekonomiks.DAL;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Ekonomiks.Controllers
{
    public class InterviewController : Controller
    {
        private readonly AppDbContext _db;
        public InterviewController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int Id)
        {
            AllViewModel vm = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                GetInterview = _db.Interviews.FirstOrDefault(interview => interview.Id == Id),
                Interviews=_db.Interviews.OrderByDescending(x => x.DateTime).Where(x => x.Id != Id)
            };
            return View(vm);
        }
        public IActionResult More()
        {
            AllViewModel vm1 = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                Interviews = _db.Interviews.OrderByDescending(x => x.DateTime)
            };
            return View(vm1);
        }
        public IActionResult Main()
        {
            
            AllViewModel vm1 = new AllViewModel()
            {
                Contacts = _db.Contacts.FirstOrDefault(contact => contact.Id == 1),
                Interviews = _db.Interviews.OrderByDescending(x => x.DateTime)
            };
            return View(vm1);


        }
    }
}