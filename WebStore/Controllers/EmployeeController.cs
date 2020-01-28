﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
 using AutoMapper;
 using WebStore.Domain.Models;
 using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    /// <summary>
    /// this controller for working with employee data
    /// </summary>
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeesService _employeesService;

        public EmployeeController(IEmployeesService employeesService, IMapper mapper)
        {
            _mapper = mapper;
            _employeesService = employeesService;
        }


        /// <summary>
        /// Displaying employee list
        /// </summary>
        /// <returns>EmployeeList html page</returns>
        [Route("employees")]
        public IActionResult EmployeeList()
        {
            var employees = _mapper.Map<IEnumerable<EmployeeViewModel>>(_employeesService.GetAll());

            return View(employees);
        } 

        /// <summary>
        /// Displaying employeeDetails
        /// </summary>
        /// <returns>EmployeeDetails html page</returns>
        [Route("employees/{id}")]
        public IActionResult EmployeeDetails(int id)
        {
            var employeeModel = _mapper.Map<EmployeeViewModel>(_employeesService.GetById(id));

            return View(employeeModel);
        }

        /// <summary>
        /// Adding or editing employee
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Edit html page</returns>
        [Route("employee_edit/{id?}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            EmployeeViewModel employeeModel;

            if (id.HasValue)
            {
                var employee = _employeesService.GetById(id.Value);

                if (employee is null)
                    return NotFound();

                employeeModel = _mapper.Map<EmployeeViewModel>(employee);
            }
            else
                employeeModel = new EmployeeViewModel();

            return View(employeeModel);
        }

        /// <summary>
        /// Redirecting EmployeeList View after additing or editing employee
        /// </summary>
        /// <param name="employeeModel">employee model</param>
        /// <returns>EmployeeList html page</returns>
        [HttpPost]
        [Route("employee_edit/{id?}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(EmployeeViewModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                if (employeeModel.Id > 0)
                {
                    var employee = _employeesService.GetById(employeeModel.Id);

                    if (employee is null)
                        return NotFound();

                    var  employeeEdit = _mapper.Map<Employee>(employeeModel);

                    _employeesService.Edit(employeeEdit.Id, employeeEdit);
                }
                else
                {
                    var employeeNew = _mapper.Map<EmployeeViewModel, Employee>(employeeModel);

                    _employeesService.AddNew(employeeNew);
                }
                return RedirectToAction(nameof(EmployeeList));
            }

            return View(employeeModel);
        }

        /// <summary>
        /// Deleting an employee
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>EmployeeList html page</returns>
        [Route("employee_delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            _employeesService.Delete(id);
            return RedirectToAction(nameof(EmployeeList));
        }
    }
}