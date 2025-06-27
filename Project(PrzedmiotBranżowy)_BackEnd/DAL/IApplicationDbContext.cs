using Microsoft.EntityFrameworkCore;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranzowy_BackEnd.DAL
{
    public interface IApplicationDbContext
    {
        public DbSet<TestUser> TestsUsers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }

        public Task<int> SaveChangesAsync();

        public int SaveChanges();
    }
}
