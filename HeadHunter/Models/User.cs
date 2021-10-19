using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class User : IdentityUser
    {
        public string Avatar { get; set; }
        public Occupation Occupation { get; set; }
    }
    public enum Occupation
    {
        Undefined,
        Employer,
        JobSeeker
    }
}
