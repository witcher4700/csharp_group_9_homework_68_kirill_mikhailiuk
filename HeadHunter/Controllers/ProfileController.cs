using HeadHunter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Controllers
{
    public class ProfileController : Controller
    {
        private HeadHunterContext _context;
        public ProfileController(HeadHunterContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user.Occupation == Occupation.Работодатель)
            {

                return RedirectToAction("VacancyIndex", new { userId = user.Id });
            }
            else
            {

                return RedirectToAction("ResumeIndex", new { userId = user.Id });
            }
        }

        public IActionResult ResumeIndex(string userId)
        {
            var resumes = _context.Resumes.Where(r => r.UserId == userId);
            return View(resumes);
        }
        public IActionResult VacancyIndex(string userId)
        {
            var vacancies = _context.Vacancies.Where(r => r.UserId == userId);
            return View(vacancies);
        }
    }
}
