using EduVerse.Core.Dtos;
using EduVerse.Data.Contract;
using EduVerse.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Implementation
{
    public class UserProfileService:IUserProfileService
    {
        private readonly IUserProfileRepository repository;

        public UserProfileService(IUserProfileRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UserDto?> GetUserInfoAsync(int userId)
        {
            return await repository.GetUserInfoAsync(userId);
        }

        public async Task UpdateUserBio(int userId,string bio)
        {
            await repository.UpdateUserBio(userId,bio);
        }

        public async Task UpdateUserProfilePicture(int userId,string pictureUrl)
        {
            await repository.UpdateUserProfilePicture(userId, pictureUrl);
        }
    }
}
