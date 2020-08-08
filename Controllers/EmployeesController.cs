using FactoryDesignPattern.Models;
using Microsoft.AspNetCore.Mvc;

namespace FactoryDesignPattern.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employee;

        public EmployeesController(IEmployeeRepository employee)
        {
            _employee = employee;
        }

        public IActionResult Index()
        {
            var employees = _employee.GetEmployees();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var savedEmployee = _employee.AddEmployee(employee);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var employee = _employee.GetEmployee(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(int id, Employee employee)
        {
            var updateEmployee = _employee.UpdateEmployee(employee);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _employee.GetEmployee(id);

            if (employee == null)
                return NotFound();

            _employee.DeleteEmployee(id);

            return RedirectToAction("Index");
        }
    }
}