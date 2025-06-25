using EduVerse.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Contract
{
    public interface IVideoRequestService
    {
        Task<List<VideoRequestDto>> GetAllAsync();
        Task<VideoRequestDto?> GetByIdAsync(int id);
        Task<IEnumerable<VideoRequestDto>> GetByUserIdAsync(int userId);
        Task<VideoRequestDto> CreateAsync(VideoRequestDto model);
        Task<VideoRequestDto> UpdateAsync(int id, VideoRequestDto model);
        Task DeleteAsync(int id);
        Task<VideoRequestDto> SendVideoRequestAckEmail(VideoRequestDto model);
    }
}
 