using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int DialogId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string MessageText { get; set; }
    }
}
