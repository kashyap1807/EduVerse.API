using EduVerse.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Contract
{
    public interface IEnrollmentService
    {
        Task<EnrollmentDto> AddEnrollmentAsync(EnrollmentDto enrollmentModel);
        Task<EnrollmentDto> GetEnrollmentByIdAsync(int id);
        Task<List<EnrollmentDto>> GetEnrollmentByUserIdAsync(int userId);
    }
}
