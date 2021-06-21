using API.Context;
using API.Models;
using API.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    //    public class EmployeeRepository : IEmployeeRepository
    //    {
    //        private readonly MyContext myContext;
    //        public EmployeeRepository(MyContext myContext)
    //        {
    //            this.myContext = myContext;
    //        }

    //        public IEnumerable<Employee> Get()
    //        {
    //            var employee = myContext.Entity.ToList();
    //            return employee;
    //        }

    //        public Employee Get(string nik)
    //        {
    //            //var find = myContext.Employees.Find(nik);
    //            //myContext.Employees.Find(find);
    //            return myContext.Entity.Find(nik);
    //        }

    //        public int Insert(Employee employee)
    //        {
    //            myContext.Entity.Add(employee);
    //            var insert = myContext.SaveChanges();
    //            return insert;
    //        }

    //        public int Update(Employee employee, string nik)
    //        {
    //            var employees = myContext.Entity.Find(nik);

    //            //if(employee.FirstName != null)
    //            //{
    //            //    employees.FirstName = employee.FirstName;
    //            //}
    //            //if (employee.LastName != null)
    //            //{
    //            //    employees.LastName = employee.LastName;
    //            //}
    //            //if (employee.Email != null)
    //            //{
    //            //    employees.Email = employee.Email;
    //            //}
    //            //if (employee.Salary != null)
    //            //{
    //            //    employees.Salary = employee.Salary;
    //            //}
    //            //if (employee.PhoneNumber != null)
    //            //{
    //            //    employees.PhoneNumber = employee.PhoneNumber;
    //            //}
    //            //if (employee.BirthDate != null)
    //            //{
    //            //    employees.BirthDate = employee.BirthDate;
    //            //}

    //            employees.FirstName = employee.FirstName;
    //            employees.LastName = employee.LastName;
    //            employees.Email = employee.Email;
    //            employees.Salary = employee.Salary;
    //            employees.PhoneNumber = employee.PhoneNumber;
    //            employees.BirthDate = employee.BirthDate;

    //            myContext.Entity.Update(employees);
    //            return myContext.SaveChanges();
    //        }

    //        public int Delete(string nik)
    //        {
    //            var find = myContext.Entity.Find(nik);
    //            if (find != null)
    //            {
    //                myContext.Entity.Remove(find);
    //            }
    //            return myContext.SaveChanges();
    //        }

    //        //public int Delete(string nik)
    //        //{
    //        //    var find = myContext.Employees.Find(nik);
    //        //    myContext.Employees.Remove(find);
    //        //    return myContext.SaveChanges();
    //        //}
    //    }
}
