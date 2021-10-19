using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class HeadHunterContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public HeadHunterContext(DbContextOptions<HeadHunterContext> options) : base(options) { }
    }
}
