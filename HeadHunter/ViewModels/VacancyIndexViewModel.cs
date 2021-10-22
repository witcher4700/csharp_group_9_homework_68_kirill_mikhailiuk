using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunter.Models;

namespace HeadHunter.ViewModels
{
    public class VacancyIndexViewModel
    {
        public int DialogCount { get; set; }
        public List<Vacancy> Vacancies { get; set; }
    }
}
