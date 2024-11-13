using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab_1.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace Lab_1.Data
{
    public class Lab1Context : DbContext
    {
        public Lab1Context (DbContextOptions<Lab1Context> options) : base(options)
        {
            Database.EnsureCreated(); // Create DB
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(st => st.ID);

            modelBuilder.Entity<Student>()
                .HasOne(st => st.Group)
                .WithMany();
                //.HasForeignKey(st => st.GroupNumber);

            modelBuilder.Entity<Session>().HasKey(ses => new { ses.StudentId, ses.ExamCode });

            modelBuilder.Entity<Session>()
                .HasOne(ses => ses.Exam)
                .WithMany()
                .HasForeignKey(ses => ses.ExamCode);

            modelBuilder.Entity<Session>()
                .HasOne(ses => ses.Student)
                .WithMany(st => st.Sessions)
                .HasForeignKey(ses => ses.StudentId);


            //modelBuilder.Entity<Student>()
            //    .HasMany(st => st.Sessions)
            //    .WithMany()
            //    .UsingEntity(ses =>
            //    {
            //        ses.Property<int>("Mark");
            //        ses.Property<int>("Semestr");
            //    });

            //modelBuilder
            //    .Entity<Student>()
            //    .HasMany(st => st.Sessions)
            //    .WithMany()
            //    .UsingEntity<Session>(
            //    ent => ent
            //        .HasOne(ses => ses.Student)
            //        .WithMany(st => st.Sessions)
            //        .HasForeignKey(ses => ses.StudentId),
            //    ent => ent
            //        .HasOne(ses => ses.Exam)
            //        .WithMany()
            //        .HasForeignKey(pt => pt.ExamCode),
            //    ent =>
            //    {
            //        ent.Property(ses => ses.Semest).HasDefaultValue(1);
            //        ent.Property(ses => ses.Mark).HasDefaultValue(3);
            //        ent.HasKey(ses => new { ses.ExamCode, ses.StudentId });
            //        ent.ToTable("Sessions");
            //    });
        }

        public DbSet<Lab_1.Models.Group> Groups { get; set; } = default!;
        public DbSet<Lab_1.Models.Student> Students { get; set; } = default!;
        public DbSet<Lab_1.Models.Session> Sessions { get; set; } = default!;
        public DbSet<Lab_1.Models.Exam> Exams { get; set; } = default!;
    }
}
