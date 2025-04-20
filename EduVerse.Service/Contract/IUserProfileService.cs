using EduVerse.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Contract
{
    public interface IUserProfileService
    {
        Task<UserDto?> GetUserInfoAsync(int userId);
        Task UpdateUserBio(int userId, string bio);
        Task UpdateUserProfilePicture(int userId, string pictureUrl);
    }
}
