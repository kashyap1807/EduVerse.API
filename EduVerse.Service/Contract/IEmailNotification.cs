using EduVerse.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Contract
{
    public interface IEmailNotification
    {
        Task<SendGrid.Response> SendEmailForContactUs(ContactMessageDto contactMessageDto);
    }
}
