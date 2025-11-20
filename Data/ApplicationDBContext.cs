using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace backend.Data
{
    public class ApplicationDBContext : IdentityDbContext<DefaultUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{Id = "Admin", Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole{Id = "User", Name = "User", NormalizedName = "USER"}
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            
            modelBuilder.Entity<CompanyStock>().Navigation(c => c.Comments).AutoInclude();
            
            modelBuilder.Entity<CompanyStock>()
                .HasIndex(s => s.Symbol);
            
            modelBuilder.Entity<CompanyStock>()
                .HasIndex(s => s.CompanyName);
        }
        public DbSet<CompanyStock> CompanyStocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

