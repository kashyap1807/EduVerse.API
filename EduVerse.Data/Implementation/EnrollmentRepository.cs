using EduVerse.Core.Models;
using EduVerse.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Data.Implementation
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly EduVerseDbContext _dbContext;
        public EnrollmentRepository(EduVerseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment)
        {
            await _dbContext.Enrollments.AddAsync(enrollment);
            await _dbContext.SaveChangesAsync();
            return enrollment;
        }

        public async Task<Enrollment> GetEnrollmentByIdAsync(int id)
        {
            return await _dbContext.Enrollments.Include(e => e.Payments).Include(e => e.Course).FirstOrDefaultAsync(e => e.EnrollmentId == id);
        }

        public async Task<List<Enrollment>> GetEnrollmentByUserIdAsync(int userId)
        {
            return await _dbContext.Enrollments.Include(e=>e.Payments).Include(e=>e.Course).Where(e=>e.UserId == userId).ToListAsync();
        }
    }
}
