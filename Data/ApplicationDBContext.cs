using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

