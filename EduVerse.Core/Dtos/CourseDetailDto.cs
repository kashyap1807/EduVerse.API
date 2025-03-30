using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Core.Dtos
{
    public class CourseDetailDto : CourseDto
    {
        public List<UserReviewDto> Reviews { get; set; } = new List<UserReviewDto>();

        public List<SessionDetailDto> SessionDetails { get; set; } = new List<SessionDetailDto>();
    }
}
