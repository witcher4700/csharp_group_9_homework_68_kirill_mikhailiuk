using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int WantedSalary { get; set; }
        public string Description { get; set; }
        public string Telegram { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public Status Status { get; set; }
        public DateTime RefreshDate { get; set; }
        public Category VacancyCategory { get; set; }
    }

}
