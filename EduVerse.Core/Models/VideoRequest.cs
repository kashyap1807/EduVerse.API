using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Core.Models
{
    public class VideoRequest
    {
        public int VideoRequestId { get; set; }

        public int UserId { get; set; }

        public string Topic { get; set; }

        public string SubTopic { get; set; }

        public string ShortTitle { get; set; }

        public string RequestDescription { get; set; }

        public string Response { get; set; }

        public string VideoUrls { get; set; }
    }
}
