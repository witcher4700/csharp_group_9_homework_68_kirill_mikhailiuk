using HeadHunter.Models;
using HeadHunter.ViewModels;
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
            if (User.Identity.IsAuthenticated)
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
            return RedirectToAction("Login", "Account");

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
        [HttpGet]
        public ActionResult EditResume(int resumeId)
        {
            var resume = _context.Resumes.Find(resumeId);
            return View(resume);
        }
        [HttpPost, ActionName("EditResume")]
        public ActionResult EditResume(Resume resume)
        {
            var r = _context.Resumes.Find(resume.Id);
            _context.Entry(r).State = EntityState.Modified;
            r.Description = resume.Description;
            r.Facebook = resume.Facebook;
            r.LinkedIn = resume.LinkedIn;
            r.Telegram = resume.Telegram;
            r.WantedSalary = resume.WantedSalary;
            r.VacancyCategory = resume.VacancyCategory;
            r.RefreshDate = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index", new { name = User.Identity.Name });
        }
        [HttpGet]
        public ActionResult EditVacancy(int vacancyId)
        {
            var vacancy = _context.Vacancies.Find(vacancyId);
            return View(vacancy);
        }
        [HttpPost, ActionName("EditVacancy")]
        public ActionResult EditVacancy(Vacancy vacancy)
        {
            var v = _context.Vacancies.Find(vacancy.Id);
            _context.Entry(v).State = EntityState.Modified;
            v.Name = vacancy.Name;
            v.ExpirienceTo = vacancy.ExpirienceTo;
            v.ExpirienceFrom = vacancy.ExpirienceFrom;
            v.Description = vacancy.Description;
            v.Category = vacancy.Category;
            v.RefreshDate = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index", new { name = User.Identity.Name });
        }
        public IActionResult ResumeDetails(int resumeId)
        {
            var resumeDetails = new ResumeDetails()
            {
                Resume = _context.Resumes.Find(resumeId)
            };
            resumeDetails.User = _context.Users.FirstOrDefault(u => u.Id == resumeDetails.Resume.UserId);
            resumeDetails.Educations = _context.Educations.Where(e => e.ResumeId == resumeDetails.Resume.Id);
            resumeDetails.Expiriences = _context.Expiriences.Where(e => e.ResumeId == resumeDetails.Resume.Id);
            return View(resumeDetails);
        }
        public IActionResult VacancyDetails(int vacancyId)
        {
            var vacancy = _context.Vacancies.Find(vacancyId);

            return View(vacancy);
        }
    }
}
