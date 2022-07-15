using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Poultry.Farm.MIS.Models
{
    //If you are using Entity Framework Core 2.1 or later there is a new method of seeding(inserting) database data.In your application DbContext class, override OnModelCreating() method.In this example, HasData() method configures Employee entity to have the specified seed data.
    //Using Migrations to seed data

    //The following command adds a new migration.I named the migration SeedEmployeesTable as we are using this migration to specifically add seed data to the Employees database table.

    //Add-Migration AnynameofMigration for example: SeedEmployeesTable


    //Finally, execute Update-Database command to apply the above migration to the database.

    //Your application DbContext class must inherit from IdentityDbContext class instead of DbContext class. This is required because IdentityDbContext provides all the DbSet properties needed to manage the identity tables in SQL Server. We will see all the tables that the asp.net core identity framework generates in just a bit. If you go through the hierarchy chain of IdentityDbContext class, you will see it inherits from DbContext class. So this is the reason you do not have to explicitly inherit from DbContext class if your class is inheriting from IdentityDbContext class.

    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

        ////Seed data by making an extension class to keep AppDbContext clean
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

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
    }
}
