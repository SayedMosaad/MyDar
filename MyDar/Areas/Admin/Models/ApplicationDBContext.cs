using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyDar.Areas.Admin.Models
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> contextOption):base(contextOption)
        {
                
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Features> Features { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public DbSet<Testemonials> Testemonials { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Request> Requests { get; set; }

    }
}
