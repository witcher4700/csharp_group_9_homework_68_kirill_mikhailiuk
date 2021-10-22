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
    public class DialogController : Controller
    {
        private HeadHunterContext _context;
        public DialogController(HeadHunterContext context)
        {
            _context = context;
        }
        public IActionResult Index(string vacancyName, string resumeName)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;

            var dialogCheck = _context.Dialogs.FirstOrDefault(d=>d.ResumeId == _context.Resumes.FirstOrDefault(r=>r.Name == resumeName).Id && d.VacancyId == _context.Vacancies.FirstOrDefault(r => r.Name == vacancyName).Id);
            
            if(dialogCheck != null)
            {
                return View(dialogCheck);
            }
            else
            {
                var dialog = new Dialog()
                {
                    VacancyId = _context.Vacancies.FirstOrDefault(r => r.Name == vacancyName).Id,
                    ResumeId = _context.Resumes.FirstOrDefault(r => r.Name == resumeName).Id,
                    FirstUserId = userId
                };
                var user = _context.Users.Find(dialog.FirstUserId);
                if (user.Occupation == Occupation.Работодатель)
                {
                    dialog.SecondUserId = _context.Resumes.FirstOrDefault(r => r.Id == dialog.ResumeId).UserId;
                }
                else
                {
                    dialog.SecondUserId = _context.Vacancies.FirstOrDefault(r => r.Id == dialog.VacancyId).UserId;
                }
                _context.Dialogs.Add(dialog);
                _context.SaveChanges();
                return View(dialog);
            }
        }
        public IActionResult GetMessages(int dialogId)
        {
            var messages = _context.Messages.Where(m => m.DialogId == dialogId).Include(m=>m.User).OrderBy(m => m.Id).ToList();
            return Json(new { messages });
        }

        [HttpPost]
        public IActionResult WriteMessage(string message, int dialogId)
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
            var user = _context.Users.Find(currentUser);
            var chatMessage = new Message()
            {
                MessageText = message,
                UserId = currentUser,
                User = _context.Users.Find(currentUser),
                DialogId = dialogId
            };
            _context.Messages.Add(chatMessage);
            _context.SaveChanges();
            return Ok();
        }
        public IActionResult GetActiveDialogs()
        {
            var dialogs = _context.Dialogs.Where(d => d.FirstUserId == _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id || d.SecondUserId == _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id).ToList();
            return View(dialogs);
        }
        public IActionResult IndexId(int vacancyId, int resumeId)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;

            var dialogCheck = _context.Dialogs.FirstOrDefault(d => d.ResumeId == resumeId && d.VacancyId == vacancyId);

            if (dialogCheck != null)
            {
                return View(dialogCheck);
            }
            else
            {
                var dialog = new Dialog()
                {
                    VacancyId = vacancyId,
                    ResumeId = resumeId,
                    FirstUserId = userId
                };
                var user = _context.Users.Find(dialog.FirstUserId);
                if (user.Occupation == Occupation.Работодатель)
                {
                    dialog.SecondUserId = _context.Resumes.FirstOrDefault(r => r.Id == dialog.ResumeId).UserId;
                }
                else
                {
                    dialog.SecondUserId = _context.Vacancies.FirstOrDefault(r => r.Id == dialog.VacancyId).UserId;
                }
                _context.Dialogs.Add(dialog);
                _context.SaveChanges();
                return View(dialog);
            }
        }
    }
}
