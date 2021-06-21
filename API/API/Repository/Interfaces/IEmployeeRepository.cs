using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interfaces
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get(); //pakai list --mengembalikan semua data dari tabel, mirip foreach
        Employee Get(string nik); //mengembalikan data berdasarkan value tertentu (nik), bedanya dari IEnum adalah kembalian datanya
        int Insert(Employee employee);
        int Update(Employee employee, string nik);
        int Delete(string nik);
    }
}
