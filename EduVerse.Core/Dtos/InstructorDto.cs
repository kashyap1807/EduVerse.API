using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Core.Dtos
{
    public class InstructorDto
    {
        public int InstructorId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Bio { get; set; }

        public int UserId { get; set; }
    }
}
