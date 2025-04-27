using EduVerse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Data.Contract
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment);
        Task<Enrollment> GetEnrollmentByIdAsync(int id);
        Task<List<Enrollment>> GetEnrollmentByUserIdAsync(int userId);
    }
}
