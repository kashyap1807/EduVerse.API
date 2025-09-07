<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>EduVerse API - README</title>
</head>
<body style="font-family: Arial, sans-serif; line-height: 1.6; margin: 20px; background-color: #f9f9f9; color: #333;">

  <h1 style="color: #2c3e50; border-bottom: 2px solid #3498db; padding-bottom: 10px;">EduVerse API</h1>
  <p>
    EduVerse is a modern learning platform designed to provide a seamless and intuitive experience 
    for both students and instructors. The API powers course management, user enrollment, payments, 
    and integrations with Azure services to deliver a secure and scalable e-learning solution.
  </p>

  <div style="background:#fff; padding:15px 20px; margin-bottom:20px; border-radius:8px; box-shadow:0 2px 6px rgba(0,0,0,0.1);">
    <h2 style="color:#2c3e50; margin-top:0; border-left:4px solid #3498db; padding-left:10px;">🌐 Production Link</h2>
    <p>
      Access the live production deployment here:  
      <a href="https://eduversebykashyap.azurewebsites.net/" target="_blank" style="color:#3498db; text-decoration:none;">https://eduversebykashyap.azurewebsites.net/</a>
    </p>
  </div>

  <div style="background:#fff; padding:15px 20px; margin-bottom:20px; border-radius:8px; box-shadow:0 2px 6px rgba(0,0,0,0.1);">
    <h2 style="color:#2c3e50; margin-top:0; border-left:4px solid #3498db; padding-left:10px;">🚀 Project Overview</h2>
    <ul style="margin:10px 0 20px 25px;">
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Students:</span> Browse, enroll, and complete courses at their own pace or join sessions.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Instructors:</span> Create, manage, and deliver courses with multimedia content.</li>
    </ul>
  </div>

  <div style="background:#fff; padding:15px 20px; margin-bottom:20px; border-radius:8px; box-shadow:0 2px 6px rgba(0,0,0,0.1);">
    <h2 style="color:#2c3e50; margin-top:0; border-left:4px solid #3498db; padding-left:10px;">🛠️ Technology Stack</h2>
    <ul style="margin:10px 0 20px 25px;">
      <li style="margin-bottom:6px;">Frontend: Angular 18</li>
      <li style="margin-bottom:6px;">Backend: .NET Core 8 with Entity Framework Core 8</li>
      <li style="margin-bottom:6px;">Database: Azure SQL Server</li>
      <li style="margin-bottom:6px;">Authentication: Azure AD B2C</li>
      <li style="margin-bottom:6px;">Storage: Azure Storage Account (for media files)</li>
      <li style="margin-bottom:6px;">Caching: In-Memory Cache</li>
      <li style="margin-bottom:6px;">Logging & Monitoring: Serilog, Application Insights, Azure Monitor</li>
      <li style="margin-bottom:6px;">Notifications: SendGrid (Email)</li>
      <li style="margin-bottom:6px;">Deployment: Azure Web App</li>
      <li style="margin-bottom:6px;">Serverless Functions: Azure Functions (for specific tasks)</li>
      <li style="margin-bottom:6px;">Secrets Management: Azure Key Vault</li>
      <li style="margin-bottom:6px;">Source Control & CI/CD: Azure DevOps</li>
    </ul>
  </div>

  <div style="background:#fff; padding:15px 20px; margin-bottom:20px; border-radius:8px; box-shadow:0 2px 6px rgba(0,0,0,0.1);">
    <h2 style="color:#2c3e50; margin-top:0; border-left:4px solid #3498db; padding-left:10px;">✨ Key Features</h2>
    <ul style="margin:10px 0 20px 25px;">
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">User Management:</span> Secure registration, authentication, and profile management via Azure AD B2C.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Course Management:</span> Instructors can create courses with multiple sessions including videos, documents, and other learning materials.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Enrollment:</span> Students can enroll in courses, track progress, and receive certificates upon completion.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Payment Integration:</span> Secure payment processing for paid courses through a payment gateway.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Notifications:</span> Automated email notifications (course updates, payment confirmations, reminders) powered by SendGrid.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Content Delivery:</span> Supports both sessions and on-demand learning.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Monitoring & Insights:</span> Performance and user behavior analytics via Application Insights.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Security:</span> Application secrets and sensitive data stored securely in Azure Key Vault.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Scalability:</span> Hosted on Azure Web App, scaling to handle large numbers of users and courses.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Automation:</span> Workflow automation using Azure Logic Apps to reduce manual intervention.</li>
    </ul>
  </div>

  <div style="background:#fff; padding:15px 20px; margin-bottom:20px; border-radius:8px; box-shadow:0 2px 6px rgba(0,0,0,0.1);">
    <h2 style="color:#2c3e50; margin-top:0; border-left:4px solid #3498db; padding-left:10px;">💡 Benefits</h2>
    <ul style="margin:10px 0 20px 25px;">
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Scalability:</span> Designed to support high user and course volume.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Security:</span> Enterprise-grade security leveraging Azure services.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">User Experience:</span> Smooth and user-friendly interface for both students and instructors.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Flexibility:</span> Supports both live and self-paced learning styles.</li>
      <li style="margin-bottom:6px;"><span style="font-weight:bold; color:#3498db;">Automation:</span> Streamlined workflows through integration with Azure services.</li>
    </ul>
  </div>

  <div style="background:#fff; padding:15px 20px; margin-bottom:20px; border-radius:8px; box-shadow:0 2px 6px rgba(0,0,0,0.1);">
    <h2 style="color:#2c3e50; margin-top:0; border-left:4px solid #3498db; padding-left:10px;">⚙️ Deployment</h2>
    <ul style="margin:10px 0 20px 25px;">
      <li style="margin-bottom:6px;">Deployed on Azure Web App</li>
      <li style="margin-bottom:6px;">CI/CD pipelines configured with Azure DevOps</li>
      <li style="margin-bottom:6px;">Infrastructure and workflows integrated with Azure Functions, Key Vault, and Logic Apps</li>
    </ul>
  </div>

</body>
</html>
