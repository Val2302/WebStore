using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;

namespace WebStore.Interfaces.Services
{
    /// <summary>
    /// Interface describe connecting EmployeeController with Database
    /// </summary>
    public interface IEmployeesService
    {
        /// <summary>
        /// Get employee list
        /// </summary>
        /// <returns></returns>
        IEnumerable<Employee> GetAll();

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>required employee or null if employee with such Id does not exist</returns>
        Employee GetById(int id);

        /// <summary>
        /// Adding new employee
        /// </summary>
        /// <param name="employee">model of new employee</param>
        void AddNew(Employee employee);

        /// <summary>
        /// Deleting employee by id
        /// </summary>
        /// <param name="id">identifier</param>
        void Delete(int id);

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="id">emloyee Id</param>
        /// <param name="newEmployee">new employee</param>
        /// <returns>updated employee</returns>
        Employee Edit(int id, Employee newEmployee);
    }
}
