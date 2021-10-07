using System.Linq;
using Ekonomiks.DAL;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ekonomiks.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _db;

        public ProjectController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AllViewModel vm = new AllViewModel
            {
                Contacts = _db.Contacts.FirstOrDefault(x => x.Id == 1),
                Projects=_db.Projects
            };
            return View(vm);
        }
        public IActionResult Detail(int id)
        {
            AllViewModel vm = new AllViewModel
            {
                Contacts = _db.Contacts.FirstOrDefault(x => x.Id == 1),
                Projects = _db.Projects.OrderByDescending(x => x.DateTime).Where(x => x.Id != id),
                GetProject = _db.Projects.FirstOrDefault(x => x.Id == id)
            };
            return View(vm);
        }
    }
}