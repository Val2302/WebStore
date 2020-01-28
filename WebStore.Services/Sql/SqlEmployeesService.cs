using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Sql
{
    /// <summary>
    /// Layer between EmployeeController and Database
    /// Responsible for getting, updating for controller and view data transfer to the database 
    /// </summary>
    public class SqlEmployeesService : IEmployeesService
    {
        private readonly WebStoreContext _context;

        public SqlEmployeesService(WebStoreContext context)
        {
            _context = context;
        }


        public IEnumerable<Employee> GetAll()
            => _context.Employees.ToList();

        public Employee GetById(int id)
            => _context.Employees.AsQueryable().FirstOrDefault(e => e.Id == id);

        public void AddNew(Employee employee)
        {
            _context.Employees.Add(employee);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = GetById(id);

            if (employee is null)
                throw new Exception("Пользователь не найден");

            _context.Employees.Remove(employee);

            _context.SaveChanges();
        }

        public Employee Edit(int id, Employee newEmployee)
        {
            var employee = GetById(newEmployee.Id);

            if (employee is null)
                throw new Exception("Пользователь не найден");

            employee.Age = newEmployee.Age;
            employee.FirstName = newEmployee.FirstName;
            employee.SecondName = newEmployee.SecondName;
            employee.Patronymic = newEmployee.Patronymic;
            employee.IsMan = newEmployee.IsMan;
            employee.Position = newEmployee.Position;
            employee.SecretName = newEmployee.SecretName;

            _context.SaveChanges();

            return employee;
        }
    }
}
