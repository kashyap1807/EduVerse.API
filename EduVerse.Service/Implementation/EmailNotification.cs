using Azure;
using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Implementation
{
    public class EmailNotification : IEmailNotification
    {
        private readonly IConfiguration configuration;

        public EmailNotification(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<SendGrid.Response> SendEmailForContactUs(ContactMessageDto contactMessageDto)
        {
            var apiKey = configuration["SendGrid:SENDGRID_API_KEY"];
            var from = new EmailAddress(configuration["SendGrid:From"]);
            var to = new EmailAddress(configuration["SendGrid:From"], "Kashyap");

            var sendGridMessage = new SendGridMessage()
            {
                From = from,
                ReplyTo = to,
                Subject = "Contact page: Received a request from user"
            };

            sendGridMessage.AddContent(MimeType.Html, GetEmailContent(contactMessageDto));
            sendGridMessage.AddTo(to);

            Console.WriteLine($"Sending email with payload: \n{sendGridMessage.Serialize()}");

            var response = await new SendGridClient(apiKey).SendEmailAsync(sendGridMessage).ConfigureAwait(false);
            Console.WriteLine($"Response: {response.StatusCode}");
            Console.WriteLine(response.Headers);

            return response;
        }

        private string GetEmailContent(ContactMessageDto contactMessageDto)
        {

            return $$"""
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset=""UTF-8"">
                        <title>An enquiry received  - {{contactMessageDto.Subject}}</title>
                    </head>
                    <body>                        
                        <p>Dear LearnSmartCoding</p>
                        <p>You have received an enquiry from a user and the details as follows.</p>
                    
                        <p><strong>Message details</strong></p>
                        <ul>
                            <li>User Name: {{contactMessageDto.Name}}</li>
                            <li>User Email: {{contactMessageDto.Email}}</li>
                    <li>Subject: {{contactMessageDto.Subject}}</li>
                    <li>Message: {{contactMessageDto.Message}}</li>
                        </ul>
                    
                    
                        <p><strong>Warm regards,</strong></p>
                        <p>LearnSmartCoding [Automated]</p>
                    </body>
                    </html>                    
                    """;
        }
    }
}
