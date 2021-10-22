using HeadHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class VacancyDetailViewModel
    {
        public Vacancy Vacancy { get; set; }
        public IEnumerable<Resume> Resumes { get; set; }
    }
}
