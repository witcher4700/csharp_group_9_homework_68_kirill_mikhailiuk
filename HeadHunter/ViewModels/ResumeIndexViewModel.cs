using HeadHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class ResumeIndexViewModel
    {
        public List<Resume> Resumes{ get; set; }
        public int DialogCount { get; set; }
    }
}
