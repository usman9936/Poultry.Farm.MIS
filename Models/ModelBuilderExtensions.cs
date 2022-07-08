using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Poultry.Farm.MIS.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        EmployeeId = 2,
                        Name = "Mary",
                        Department = Departments.IT,
                        Email = "mary@pragimtech.com"
                    },
                    new Employee
                    {
                        EmployeeId = 3,
                        Name = "John",
                        Department = Departments.HR,
                        Email = "john@pragimtech.com"
                    }
                );
        }
    }
}
