using Microsoft.AspNetCore.Http;
using Poultry.Farm.MIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poultry.Farm.MIS.ViewModels
{
    public class EmployeeCreateViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Departments Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
