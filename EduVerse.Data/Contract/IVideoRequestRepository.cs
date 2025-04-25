using EduVerse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Data.Contract
{
    public interface IVideoRequestRepository
    {
        Task<IEnumerable<VideoRequest>> GetAllAsync();
    }
}
