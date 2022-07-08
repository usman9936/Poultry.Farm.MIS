using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poultry.Farm.MIS.Models
{
    //If you are using Entity Framework Core 2.1 or later there is a new method of seeding(inserting) database data.In your application DbContext class, override OnModelCreating() method.In this example, HasData() method configures Employee entity to have the specified seed data.
    //Using Migrations to seed data

    //The following command adds a new migration.I named the migration SeedEmployeesTable as we are using this migration to specifically add seed data to the Employees database table.

    //Add-Migration AnynameofMigration for example: SeedEmployeesTable


    //Finally, execute Update-Database command to apply the above migration to the database.

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

        
        //Seed data within AppDbContext File
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee>().HasData(
        //        new Employee
        //        {
        //            EmployeeId = 2,
        //            Name = "Mary",
        //            Department = Departments.IT,
        //            Email = "mary@pragimtech.com"
        //        },
        //        new Employee
        //        {
        //            EmployeeId = 3,
        //            Name = "John",
        //            Department = Departments.HR,
        //            Email = "john@pragimtech.com"
        //        }
        //    );
        //}

        ////Seed data by making an extension class to keep AppDbContext clean
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.Seed();
        }
    }
}
