using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interfaces
{
    interface IUniversityRepository
    {
        IEnumerable<University> Get(); //pakai list --mengembalikan semua data dari tabel, mirip foreach
        Employee Get(int universityId); //mengembalikan data berdasarkan value tertentu (nik), bedanya dari IEnum adalah kembalian datanya
        int Insert(University profiling);
        int Update(University profiling, int universityId);
        int Delete(int universityId);
    }
}
