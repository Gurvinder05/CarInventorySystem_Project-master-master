using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarInventorySystem_Project.Models;

namespace CarInventorySystem_Project.Data
{
    public class CarInventorySystem_ProjectContext : DbContext
    {
        public CarInventorySystem_ProjectContext(DbContextOptions<CarInventorySystem_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<CarInventorySystem_Project.Models.Brand> Brand { get; set; }
        public DbSet<CarInventorySystem_Project.Models.CarModel> CarModel { get; set; }
        public DbSet<CarInventorySystem_Project.Models.Car> Car { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().
                HasOne(c => c.Model).
                WithMany(p => p.Cars).
                OnDelete(DeleteBehavior.Restrict);


        }
    }
}
