using HeadHunter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Controllers
{
    public class VacancyController : Controller
    {
        private HeadHunterContext _context;
        public VacancyController(HeadHunterContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Add(Vacancy vacancy)
        {
            if (vacancy != null)
            {
                var userName = User.Identity.Name;
                vacancy.UserId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
                vacancy.RefreshDate = DateTime.Now;
                vacancy.Status = Status.InStock;
                _context.Vacancies.Add(vacancy);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
