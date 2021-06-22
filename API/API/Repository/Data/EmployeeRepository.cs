using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        //private readonly DbSet<Entity> entities;

        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            Employee employee = new Employee();
            Account account = new Account();
            Education education = new Education();
            Profiling profiling = new Profiling();

            var checkNik = myContext.Employees.Find(registerVM.NIK);
            if (checkNik == null)
            {
                var checkEmail = myContext.Employees.Where(e => e.Email == registerVM.Email).FirstOrDefault<Employee>();
                if (checkEmail == null)
                {
                    employee.NIK = registerVM.NIK;
                    employee.FirstName = registerVM.FirstName;
                    employee.LastName = registerVM.LastName;
                    employee.Email = registerVM.Email;
                    employee.Salary = registerVM.Salary;
                    employee.PhoneNumber = registerVM.PhoneNumber;
                    employee.BirthDate = registerVM.BirthDate;
                    employee.Gender = (Employee.GenderType)registerVM.Gender;

                    profiling.NIK = registerVM.NIK;
                    profiling.EducationId = registerVM.EducationID;

                    account.NIK = registerVM.NIK;
                    account.Password = registerVM.Password;

                    education.Degree = registerVM.Degree;
                    education.GPA = registerVM.GPA;
                    education.UniversityId = registerVM.UniversityID;

                    myContext.Employees.Add(employee);
                    myContext.Accounts.Add(account);
                    myContext.Profilings.Add(profiling);
                    myContext.Educations.Add(education);
                    var insert = myContext.SaveChanges();

                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
