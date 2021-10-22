using HeadHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class DialogViewModel
    {
        public Dialog Dialog { get; set; }
        public List<Message> Messages { get; set; }
    }
}
