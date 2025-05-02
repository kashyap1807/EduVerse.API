using AutoMapper;
using EduVerse.Core.Dtos;
using EduVerse.Core.Models;

namespace EduVerse.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VideoRequest, VideoRequestDto>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName}, {src.User.LastName}"));

            CreateMap<VideoRequestDto, VideoRequest>()
                .ForMember(dest => dest.User, opt => opt.Ignore()); // We don't map User here since it's handled separately

            CreateMap<EnrollmentDto, Enrollment>();
            CreateMap<Enrollment, EnrollmentDto>()
            .ForMember(dest => dest.CoursePaymentDto,
                opt => opt.MapFrom(src =>
                    src.Payments.OrderByDescending(o => o.PaymentDate).FirstOrDefault()))
            .ForMember(dest => dest.CourseTitle,
                opt => opt.MapFrom(src => src.Course.Title));  // Mapping for CourseTitle


            CreateMap<CoursePaymentDto, Payment>();
            CreateMap<Payment, CoursePaymentDto>();

            CreateMap<Review, UserReviewDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.LastName}, {src.User.FirstName}"));

            CreateMap<UserReviewDto, Review>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Course, opt => opt.Ignore());

            //CreateMap<InstructorDto, Instructor>();
            //CreateMap<Instructor, InstructorDto>();
        }
    }
}
