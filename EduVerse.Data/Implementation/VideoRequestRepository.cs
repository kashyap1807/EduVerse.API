using EduVerse.Core.Dtos;
using EduVerse.Core.Models;
using EduVerse.Data.Contract;
using Microsoft.EntityFrameworkCore;

namespace EduVerse.Data.Implementation
{
    public class VideoRequestRepository : IVideoRequestRepository
    {
        private readonly EduVerseDbContext dbContext;

        public VideoRequestRepository(EduVerseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<VideoRequest> AddAsync(VideoRequest videoRequest)
        {
            dbContext.VideoRequests.Add(videoRequest);
            await dbContext.SaveChangesAsync();
            return videoRequest;
        }

        public async Task DeleteAsync(int id)
        {
            var videoRequest = await GetByIdAsync(id);
            if (videoRequest != null)
            {
                dbContext.VideoRequests.Remove(videoRequest);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Video request with ID {id} not found.");
            }

        }

        public async Task<IEnumerable<VideoRequest>> GetAllAsync()
        {
            return await dbContext.VideoRequests.Include(v => v.User).ToListAsync();
        }

        public Task<VideoRequest?> GetByIdAsync(int id)
        {
            return dbContext.VideoRequests.Include(v => v.User).FirstOrDefaultAsync(v => v.VideoRequestId == id);
        }

        public async Task<IEnumerable<VideoRequest>> GetByUserIdAsync(int userId)
        {
            return await dbContext.VideoRequests.Include(v => v.User).Where(v => v.UserId == userId).ToListAsync();
        }

        public async Task<VideoRequest> UpdateAsync(VideoRequest videoRequest)
        {
            dbContext.VideoRequests.Update(videoRequest);
            await dbContext.SaveChangesAsync();
            return videoRequest;
        }
    }
}
