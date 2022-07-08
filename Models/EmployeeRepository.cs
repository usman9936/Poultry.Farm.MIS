using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poultry.Farm.MIS.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public EmployeeRepository()
        {
            _employeeList = new List<Employee>()
                {
                     new Employee()
                     {   EmployeeId = 1,
                        Name = "Usman",
                        Email = "test@email.com",
                        Department =Departments.IT
                     },

                    new Employee()
                    {
                        EmployeeId = 2,
                        Name = "Titus",
                        Email = "test@email.com",
                        Department =Departments.HR
                    },
                    new Employee()
                    {

                        EmployeeId = 3,
                        Name = "Ali",
                        Email = "test@email.com",
                        Department = Departments.Sales
                    }
                };
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = _employeeList.Max(e => e.EmployeeId) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.EmployeeId ==id);      
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeList;
        }

        public Employee Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
