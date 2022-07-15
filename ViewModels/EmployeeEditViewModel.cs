using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poultry.Farm.MIS.ViewModels
{
    public class EmployeeEditViewModel: EmployeeCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
