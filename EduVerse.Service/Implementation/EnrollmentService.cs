
using AutoMapper;
using EduVerse.Core.Dtos;
using EduVerse.Core.Models;
using EduVerse.Data.Contract;
using EduVerse.Service.Contract;

namespace EduVerse.Service.Implementation
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;
        private readonly IMapper _mapper;
        public EnrollmentService(IEnrollmentRepository _repository,IMapper _mapper)
        {
            this._repository = _repository;          
            this._mapper = _mapper; 
        }

        public async Task<EnrollmentDto> AddEnrollmentAsync(EnrollmentDto enrollmentModel)
        {
            var enrollment = _mapper.Map<Enrollment>(enrollmentModel);
            enrollment.EnrollmentDate = DateTime.UtcNow; 
            enrollment.PaymentStatus = "Completed";

            // Create a new payment
            var payment = new Payment
            {
                EnrollmentId = enrollment.EnrollmentId,
                Amount = 0,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = enrollmentModel.CoursePaymentDto.PaymentMethod,
                PaymentStatus = "Completed" 
            };

            enrollment.Payments.Add(payment);
            var result = await _repository.AddEnrollmentAsync(enrollment);
            return _mapper.Map<EnrollmentDto>(result);
        }

        public async Task<EnrollmentDto> GetEnrollmentByIdAsync(int id)
        {
            var enrollment = await _repository.GetEnrollmentByIdAsync(id);
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<List<EnrollmentDto>> GetEnrollmentByUserIdAsync(int userId)
        {
            var enrollments = await _repository.GetEnrollmentByUserIdAsync(userId);
            return _mapper.Map<List<EnrollmentDto>>(enrollments);
        }

    }
}
