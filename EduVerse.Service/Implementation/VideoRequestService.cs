﻿using AutoMapper;
using EduVerse.Core.Dtos;
using EduVerse.Core.Models;
using EduVerse.Data.Contract;
using EduVerse.Service.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Implementation
{
    public class VideoRequestService : IVideoRequestService
    {
        private readonly IVideoRequestRepository repository;      
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<VideoRequestService> logger;
        private readonly HttpClient httpClient;

        public VideoRequestService(IVideoRequestRepository repository,IMapper mapper,IConfiguration configuration, ILogger<VideoRequestService> logger, HttpClient httpClient)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
            this.httpClient = httpClient;
        }

        public async Task<VideoRequestDto> CreateAsync(VideoRequestDto model)
        {
            var videoRequest = mapper.Map<VideoRequest>(model);
            var createdVideoRequest = await repository.AddAsync(videoRequest);
            return mapper.Map<VideoRequestDto>(createdVideoRequest);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(id);
        }

        public async Task<List<VideoRequestDto>> GetAllAsync()
        {
            var videoRequests = await repository.GetAllAsync();
            return mapper.Map<List<VideoRequestDto>>(videoRequests);
        }

        public async Task<VideoRequestDto> GetByIdAsync(int id)
        {
            var videoRequests = await repository.GetByIdAsync(id);
            return videoRequests == null ? null : mapper.Map<VideoRequestDto>(videoRequests);
        }

        public async Task<IEnumerable<VideoRequestDto>> GetByUserIdAsync(int userId)
        {
            var  videoRequests = await repository.GetByUserIdAsync(userId);
            return mapper.Map<IEnumerable<VideoRequestDto>>(videoRequests);
        }

        public async Task<VideoRequestDto> UpdateAsync(int id, VideoRequestDto model)
        {
            var existingVideoRequest = await repository.GetByIdAsync(id);
            if (existingVideoRequest == null)
            {
                throw new KeyNotFoundException($"Video request with ID {id} not found.");
            }
            model.UserId = existingVideoRequest.UserId;
            mapper.Map(model, existingVideoRequest);
            var updatedVideoRequest = await repository.UpdateAsync(existingVideoRequest);
            return mapper.Map<VideoRequestDto>(updatedVideoRequest);
        }

        public async Task<VideoRequestDto> SendVideoRequestAckEmail(VideoRequestDto model)
        {
            var videoModelRequest = new VideoRequestDto() { VideoRequestId = model.VideoRequestId };
            var _functionUrl = configuration["AzureFunction:VideoRequestTriggerUrl"];
            logger.LogInformation("_functionUrl:" + _functionUrl);

            var response = await httpClient.PostAsJsonAsync(_functionUrl, videoModelRequest);

            logger.LogInformation($"response code: {response.StatusCode}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<VideoRequestDto>();
        }
    }
}
