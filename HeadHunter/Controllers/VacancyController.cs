using HeadHunter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var vacancies = _context.Vacancies.Where(v => v.Status == Status.InPublic).OrderByDescending(r => r.RefreshDate);
            return View(vacancies);
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
            return RedirectToAction("Index", "Resume");
        }
        public IActionResult ChangeStatus(int vacancyId)
        {
            var vacancy = _context.Vacancies.Find(vacancyId);
            _context.Entry(vacancy).State = EntityState.Modified;
            if (vacancy.Status == Status.InPublic)
            {
                vacancy.Status = Status.InStock;
            }
            else
            {
                vacancy.Status = Status.InPublic;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult RefreshDate(int vacancyId)
        {
            var vacancy = _context.Vacancies.Find(vacancyId);
            _context.Entry(vacancy).State = EntityState.Modified;
            vacancy.RefreshDate = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int vacancyId)
        {
            var vacancy = _context.Vacancies.Find(vacancyId);
            
            return View(vacancy);
        }
    }
}
