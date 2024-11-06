using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab_1.Models;
using Microsoft.Extensions.Hosting;

namespace Lab_1.Data
{
    public class Lab1Context : DbContext
    {
        public Lab1Context (DbContextOptions<Lab1Context> options)
            : base(options)
        {
            Database.EnsureCreated(); // создаёт базу данных
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
                .HasMany(ex => ex.Students)
                .WithMany();
        }

        public DbSet<Lab_1.Models.Student> Students { get; set; } = default!;
        public DbSet<Lab_1.Models.Exam> Exams { get; set; } = default!;
    }
}
