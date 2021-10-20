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
            var resumes = _context.Resumes.Where(v => v.Status == Status.InPublic);
            return View(resumes);
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
            resumeDetails.User = _context.Users.FirstOrDefault(u=>u.Id == resumeDetails.Resume.UserId);
            resumeDetails.Educations = _context.Educations.Where(e => e.ResumeId == resumeDetails.Resume.Id);
            resumeDetails.Expiriences = _context.Expiriences.Where(e => e.ResumeId == resumeDetails.Resume.Id);
            return View(resumeDetails);
        }
    }
}
