using HeadHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class ResumeDetails
    {
        public User User { get; set; }
        public Resume Resume { get; set; }
        public IEnumerable<Expirience> Expiriences { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Vacancy> Vacancies { get; set; }
    }
}
