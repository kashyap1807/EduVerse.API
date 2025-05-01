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
    public class ReviewRepository : IReviewRepository
    {
        private readonly EduVerseDbContext dbContext;

        public ReviewRepository(EduVerseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await dbContext.Reviews.Include(r => r.User).Include(r => r.Course).FirstOrDefaultAsync(r => r.ReviewId == id);
        }

        public async Task<IEnumerable<Review>> GetReviewByCourseIdAsync(int courseId)
        {
            return await dbContext.Reviews.Include(r => r.User).Where(r => r.CourseId == courseId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewByUserIdAsync(int userId)
        {
            return await dbContext.Reviews.Include(r=>r.Course).Where(r=>r.UserId == userId).ToListAsync();
        }

        public async Task AddReviewAsync(Review review)
        {
            await dbContext.Reviews.AddAsync(review);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(Review review)
        {
            dbContext.Reviews.Update(review);
            await dbContext.SaveChangesAsync();
        }

       
        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await dbContext.Reviews.FindAsync(reviewId);
            if(review != null)
            {
                dbContext.Reviews.Remove(review);
                await dbContext.SaveChangesAsync();
            }            
        }
    }
}
