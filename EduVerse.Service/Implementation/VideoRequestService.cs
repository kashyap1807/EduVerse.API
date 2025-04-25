using AutoMapper;
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
    public class VideoRequestService : IVideoRequestService
    {
        private readonly IVideoRequestRepository repository;        

        public VideoRequestService(IVideoRequestRepository repository)
        {
            this.repository = repository;            
        }

        public async Task<List<VideoRequestDto>> GetAllAsync()
        {
            var videoRequest = await repository.GetAllAsync();
            var videoDto = videoRequest.Select(v => new VideoRequestDto
            {
                VideoRequestId = v.VideoRequestId,
                UserId = v.UserId,
                UserName = v.User.FirstName,
                Topic = v.Topic,
                SubTopic = v.SubTopic,
                RequestStatus = v.RequestStatus,
                ShortTitle = v.ShortTitle,
                RequestDescription = v.RequestDescription,
                Response = v.Response,
                VideoUrls = v.VideoUrls
            }).ToList();

            return videoDto;
        }
    }
}
