using ExperienceIT.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpicePractice.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpicePractice.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<MenuItem> ManuItem { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
