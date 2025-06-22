using Microsoft.EntityFrameworkCore;
using Project_PrzedmiotBranżowy_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranżowy_.DAL
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString = System.Configuration
                                                        .ConfigurationManager
                                                        .ConnectionStrings["DbConnection"]
                                                        .ConnectionString;

        public DbSet<TestUser> TestsUsers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
