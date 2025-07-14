using EduVerse.Core.Dtos;
using EduVerse.Core.Models;
using EduVerse.Data.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduVerse.Data.Implementation
{
    public class VideoRequestRepository : IVideoRequestRepository
    {
        private readonly EduVerseDbContext dbContext;
        private readonly ILogger<VideoRequestRepository> Logger;

        public VideoRequestRepository(EduVerseDbContext dbContext,ILogger<VideoRequestRepository> Logger)
        {
            this.dbContext = dbContext;
            this.Logger = Logger;
        }

        public async Task<VideoRequest> AddAsync(VideoRequest videoRequest)
        {
            
            try
            {
                dbContext.VideoRequests.Add(videoRequest);
                await dbContext.SaveChangesAsync();
                return videoRequest;
            }
            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message;
                Logger.LogError("DB Update Error: {Message}", innerMessage);
                throw; // rethrow to preserve stack trace
            }
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
