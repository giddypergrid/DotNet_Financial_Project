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
            
            modelBuilder.Entity<CompanyStock>()
                .HasIndex(s => s.Symbol);
            
            modelBuilder.Entity<CompanyStock>()
                .HasIndex(s => s.CompanyName);
            
            modelBuilder.Entity<Portfolio>()
                .HasKey(p => new { p.DefaultUserId, p.CompanyStockId });
            
            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.DefaultUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.DefaultUserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.CompanyStock)
                .WithMany(s => s.Portfolios)
                .HasForeignKey(p => p.CompanyStockId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<CompanyStock> CompanyStocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
    }
}

