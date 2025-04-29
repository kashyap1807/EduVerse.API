using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Core.Dtos
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }

        public int CourseId { get; set; }
        public string? CourseTitle { get; set; } = string.Empty;

        public int UserId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string PaymentStatus { get; set; } = null!;
        public CoursePaymentDto? CoursePaymentDto { get; set; }
    }

    public class CoursePaymentDto
    {
        public int PaymentId { get; set; }

        public int EnrollmentId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public string PaymentStatus { get; set; } = null!;
    }
}
