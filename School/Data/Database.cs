using School.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace School.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "database.db");

            optionsBuilder.UseSqlite($"Data Source={databasePath}"); //set the path of the database 
        }
    }
}