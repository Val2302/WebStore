using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/employees"),
    ApiController,
    Produces("application/json")]
    public class EmployeesApiController : ControllerBase, IEmployeesService
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesApiController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet, ActionName("Get")]
        public IEnumerable<Employee> GetAll() => _employeesService.GetAll();


        [HttpGet("{id}"), ActionName("Get")]
        public Employee GetById(int id) => _employeesService.GetById(id);

        [HttpPost, ActionName("Post")]
        public void AddNew([FromBody]Employee employee) => _employeesService.AddNew(employee);

        [HttpDelete("{id}")]
        public void Delete(int id) => _employeesService.Delete(id);

        [HttpPut("{id}"), ActionName("Put")]
        public Employee Edit(int id, [FromBody]Employee newEmployee) => _employeesService.Edit(id, newEmployee);
    }
}