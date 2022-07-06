using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poultry.Farm.MIS.Models
{
   public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetEmployees();
        Employee CreateEmployee(Employee employee);
    }
}
