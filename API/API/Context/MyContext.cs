using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext //menghubungkan Apps dgn Db
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}

        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profiling> Profilings { get; set; } 
        public DbSet<University> Universities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Education>()
                .HasOne(u => u.University)
                .WithMany(e => e.Educations);            
                //.HasForeignKey(u => u.University);

            modelBuilder.Entity<Profiling>()
                .HasOne(e => e.Education)
                .WithMany(p => p.Profilings);

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(e => e.Employee)
                .HasForeignKey<Account>(e => e.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(p => p.Profiling)
                .WithOne(a => a.Account)
                .HasForeignKey<Profiling>(a => a.NIK);

            modelBuilder.Entity<Employee>()
                .Property(s => s.Gender)
                .HasConversion<string>();
        }
                
    }
}
