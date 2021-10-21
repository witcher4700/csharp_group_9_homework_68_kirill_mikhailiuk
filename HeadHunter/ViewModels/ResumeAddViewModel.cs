using HeadHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class ResumeAddViewModel
    {
        public int WantedSalary { get; set; }
        public string Description { get; set; }
        public string Telegram { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public Category VacancyCategory { get; set; }
        public List<EducationViewModel> Educations { get; set; }
        public List<ExpirienceViewModel> Expiriences { get; set; }
    }
}
