using EduVerse.Core.Dtos;
using EduVerse.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Data.Implementation
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly EduVerseDbContext dbcontext;

        public UserProfileRepository(EduVerseDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<UserDto?> GetUserInfoAsync(int userId)
        {
            var user = await dbcontext.UserProfiles.Include(u=>u.Instructors).FirstOrDefaultAsync(f=>f.UserId == userId);
            if (user != null)
            {
                var userInfo = new UserDto()
                {
                    UserId = user.UserId,
                    Bio = "",
                    ProfilePictureUrl = user.ProfilePictureUrl,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    UserRoleModel = new List<UserRoleDto>()
                };
                if(user.Instructors.Any())
                {
                    userInfo.Bio = user.Instructors.FirstOrDefault().Bio;
                }
                return userInfo;
            }
            return null;
        }

        public async Task UpdateUserBio(int userId,string bio)
        {
            var user = await dbcontext.Instructors.FirstOrDefaultAsync(u => u.UserId == userId);
            if(user != null)
            {
                user.Bio = bio;
                await dbcontext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserProfilePicture(int userId,string pictureUrl)
        {
            var user = await dbcontext.UserProfiles.FindAsync(userId);
            if(user != null)
            {
                user.ProfilePictureUrl = pictureUrl;
                await dbcontext.SaveChangesAsync();
            }
        }
    }
}
