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
    public class ResumeController : Controller
    {
        private HeadHunterContext _context;
        public ResumeController(HeadHunterContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var resumes = _context.Resumes.Where(v => v.Status == Status.InPublic).OrderByDescending(r=>r.RefreshDate);
            return View(resumes);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult ChangeStatus(int resumeId)
        {
            var resume = _context.Resumes.Find(resumeId);
            _context.Entry(resume).State = EntityState.Modified;
            if (resume.Status == Status.InPublic)
            {
                resume.Status = Status.InStock;
            }
            else
            {
                resume.Status = Status.InPublic;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult RefreshDate(int resumeId)
        {
            var resume = _context.Resumes.Find(resumeId);
            _context.Entry(resume).State = EntityState.Modified;
            resume.RefreshDate = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int resumeId)
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

        public IActionResult AddResume(ResumeAddViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var resume = new Resume()
            {
                UserId = user.Id,
                Description = model.Description,
                WantedSalary = model.WantedSalary,
                VacancyCategory = model.VacancyCategory,
                Telegram = model.Telegram,
                Facebook = model.Facebook,
                LinkedIn = model.LinkedIn,
                Status = Status.InStock,
                RefreshDate = DateTime.Now
            };
            _context.Resumes.Add(resume);
            _context.SaveChanges();
            var educationList = new List<Education>();
            for (int i = 0; i < model.Educations.Count; i++)
            {
                var education = new Education()
                {
                    ResumeId = resume.Id,
                    Description = model.Educations[i].Description,
                    Name = model.Educations[i].Name
                };
                educationList.Add(education);
            }
            var expirienceList = new List<Expirience>();
            for (int i = 0; i < model.Expiriences.Count; i++)
            {
                var expirience = new Expirience()
                {
                    ResumeId = resume.Id,
                    Responsibilities = model.Expiriences[i].Responsibilities,
                    Name = model.Expiriences[i].Name,
                    MonthCount = model.Expiriences[i].MonthCount,
                    Position = model.Expiriences[i].Position
                };
                expirienceList.Add(expirience);
            }
            _context.Expiriences.AddRange(expirienceList);
            _context.Educations.AddRange(educationList);
            _context.SaveChanges();
            return RedirectToAction("Index", "Vacancy");
        }
    }
}
