using HeadHunter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Controllers
{
    public class ResumeController : Controller
    {
        private HeadHunterContext _context;
        public ResumeController(HeadHunterContext context)
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

        public IActionResult Add(Resume resume)
        {
            if (resume != null)
            {
                var userName = User.Identity.Name;
                resume.UserId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
                resume.RefreshDate = DateTime.Now;
                resume.Status = Status.InStock;
                _context.Resumes.Add(resume);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult AddEducation(int resumeId)
        {
            var education = new Education()
            {
                ResumeId = resumeId
            };
            return View(education);
        }

        [HttpPost]

        public IActionResult AddEducation(Education education)
        {
            if (education != null)
            {
                _context.Educations.Add(education);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult AddExpirience(int resumeId)
        {
            var education = new Education()
            {
                ResumeId = resumeId
            };
            return View(education);
        }

        [HttpPost]

        public IActionResult AddExpirience(Expirience expirience)
        {
            if (expirience != null)
            {
                _context.Expiriences.Add(expirience);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
