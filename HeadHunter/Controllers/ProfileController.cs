using HeadHunter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var resumes = _context.Resumes.Where(r => r.UserId == userId).OrderByDescending(r => r.RefreshDate);
            return View(resumes);
        }
        public IActionResult VacancyIndex(string userId)
        {
            var vacancies = _context.Vacancies.Where(r => r.UserId == userId).OrderByDescending(r => r.RefreshDate);
            return View(vacancies);
        }
        public IActionResult ChooseRole()
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user.Occupation == Occupation.Работодатель)
            {
                return RedirectToAction("Index", "Resume");
            }
            else
            {
                return RedirectToAction("Index", "Vacancy");
            }
        }
        public ActionResult Edit()
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            return View(user);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(User user)
        {
            var u = _context.Users.Find(user.Id);
            _context.Entry(u).State = EntityState.Modified;
            u.PhoneNumber = user.PhoneNumber;
            u.Avatar = user.Avatar;
            _context.SaveChanges();
            return RedirectToAction("Index", new { name = u.UserName });
        }
    }
}
