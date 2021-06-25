using API.Context;
using API.Models;
using API.ViewModels;
using BCrypt.Net;
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

        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public int Register(RegisterVM registerVM)
        {
            Employee employee = new Employee();
            Account account = new Account();
            AccountRole accountRole = new AccountRole();
            Role role = new Role();
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
                    myContext.Employees.Add(employee);
                    myContext.SaveChanges();

                    account.NIK = registerVM.NIK;
                    account.Password= BCrypt.Net.BCrypt.HashPassword(registerVM.Password, GetRandomSalt());
                    myContext.Accounts.Add(account);
                    myContext.SaveChanges();

                    accountRole.RoleID = "R1";
                    accountRole.NIK = registerVM.NIK;
                    myContext.AccountRoles.Add(accountRole);
                    myContext.SaveChanges();

                    education.Degree = registerVM.Degree;
                    education.GPA = registerVM.GPA;
                    education.UniversityId = registerVM.UniversityID;
                    myContext.Educations.Add(education);
                    myContext.SaveChanges();

                    profiling.NIK = registerVM.NIK;
                    profiling.EducationId = registerVM.EducationID;
                    myContext.Profilings.Add(profiling);
                    myContext.SaveChanges();

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

        public IQueryable ViewRegistered()
        {
            var view = (from em in myContext.Employees
                        join ac in myContext.Accounts on em.NIK equals ac.NIK
                        join pr in myContext.Profilings on ac.NIK equals pr.NIK
                        join ed in myContext.Educations on pr.EducationId equals ed.EducationId
                        join un in myContext.Universities on ed.UniversityId equals un.UniversityId
                        select new
                        {
                            em.NIK,
                            em.FirstName,
                            em.LastName,
                            em.Email,
                            em.PhoneNumber,
                            em.Salary,
                            em.BirthDate,
                            em.Gender,
                            ac.Password,
                            ed.Degree,
                            ed.GPA,
                            un.UniversityId,
                            un.Name
                        });
            return view;
        }

        public IQueryable ViewRegisteredbyID(string nik)
        {
            var find = myContext.Employees.Find(nik);

            if(find != null)
            {
                        var view = (from em in myContext.Employees
                        join ac in myContext.Accounts on em.NIK equals ac.NIK
                        join pr in myContext.Profilings on ac.NIK equals pr.NIK
                        join ed in myContext.Educations on pr.EducationId equals ed.EducationId
                        join un in myContext.Universities on ed.UniversityId equals un.UniversityId
                        where em.NIK == nik
                        select new
                        {
                            em.NIK,
                            em.FirstName,
                            em.LastName,
                            em.Email,
                            em.PhoneNumber,
                            em.Salary,
                            em.BirthDate,
                            em.Gender,
                            ac.Password,
                            ed.Degree,
                            ed.GPA,
                            un.UniversityId,
                            un.Name
                        }) ;
            return view;
            }
            else
            {
                return null;
            }
        }
    }
}
