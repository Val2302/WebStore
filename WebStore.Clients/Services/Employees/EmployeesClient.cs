using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Services.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesService
    {
        protected sealed override string ServiceAddress { get; set; }

        public EmployeesClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/employees";
        }

        
        public IEnumerable<Employee> GetAll()
        {
            var url = $"{ServiceAddress}";
            var list = Get<List<Employee>>(url);
            return list;
        }

        public Employee GetById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<Employee>(url);
            return result;
        }

        public void AddNew(Employee employee)
        {
            var url = $"{ServiceAddress}";
            Post(url, employee);
        }

        public void Delete(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            Delete(url);
        }

        public Employee Edit(int id, Employee newEmployee)
        {
            var url = $"{ServiceAddress}/{id}";
            var response = Put(url, newEmployee);
            var result = response.Content.ReadAsAsync<Employee>().Result;
            return result;
        }
    }
}
