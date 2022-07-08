using Microsoft.AspNetCore.Mvc;
using Poultry.Farm.MIS.Models;
using Poultry.Farm.MIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poultry.Farm.MIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public ViewResult Index()
        {
            var model = _employeeRepository.GetEmployees();
            return View(model);
            
        }

        public ViewResult Details(int? id)
        {            
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("Details", new { id = newEmployee.EmployeeId });
            }
            return View();
        }


        public ViewResult SampleActionMethod()
        {
            Employee EmployeeDetail = _employeeRepository.GetEmployee(1);

            //return View(EmployeeDetail);//This will return a view with the same name of Action Method which is Details()
            //return View("AnyotherViewName");//Any other view with the name present in View/Home/...
            // return View("AnyPath/AnyotherViewName.cshtml"); //Specify full path of the view

            //return View(EmployeeDetail);

            ViewData["Employee"] = EmployeeDetail; //ViewData is Dictionary and can store key and value
            ViewData["PageTitle"] = "Employee Details";
            //return View();

            ViewBag.Employee = EmployeeDetail;
            ViewBag.PageTitle= "Employee Details";
            //return View();

            

            //Passing Data with ViewModel class
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = _employeeRepository.GetEmployee(1),
                PageTitle = "Employee Details"
            };
            return View("Sample", homeDetailsViewModel);


        }
    }
}
