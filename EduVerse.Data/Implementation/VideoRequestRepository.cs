using EduVerse.Core.Dtos;
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
    public class VideoRequestRepository : IVideoRequestRepository
    {
        private readonly EduVerseDbContext dbContext;

        public VideoRequestRepository(EduVerseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<VideoRequest>> GetAllAsync()
        {
            return await dbContext.VideoRequests.Include(v => v.User).ToListAsync();
        }
    }
}
