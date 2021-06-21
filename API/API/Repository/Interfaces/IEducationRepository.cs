using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interfaces
{
    interface IEducationRepository
    {
        IEnumerable<Education> Get(); //pakai list --mengembalikan semua data dari tabel, mirip foreach
        Employee Get(int educationId); //mengembalikan data berdasarkan value tertentu (nik), bedanya dari IEnum adalah kembalian datanya
        int Insert(Education education);
        int Update(Education education, int educationId);
        int Delete(int educationId);
    }
}
