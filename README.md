<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>EduVerse API - README</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.6;
      margin: 20px;
      background-color: #f9f9f9;
      color: #333;
    }
    h1, h2 {
      color: #2c3e50;
    }
    h1 {
      border-bottom: 2px solid #3498db;
      padding-bottom: 10px;
    }
    h2 {
      margin-top: 30px;
      border-left: 4px solid #3498db;
      padding-left: 10px;
    }
    ul {
      margin: 10px 0 20px 25px;
    }
    li {
      margin-bottom: 6px;
    }
    .section {
      background: #fff;
      padding: 15px 20px;
      margin-bottom: 20px;
      border-radius: 8px;
      box-shadow: 0 2px 6px rgba(0,0,0,0.1);
    }
    .highlight {
      font-weight: bold;
      color: #3498db;
    }
  </style>
</head>
<body>

  <h1>EduVerse API</h1>
  <p>
    EduVerse is a modern learning platform designed to provide a seamless and intuitive experience 
    for both students and instructors. The API powers course management, user enrollment, payments, 
    and integrations with Azure services to deliver a secure and scalable e-learning solution.
  </p>

  <div class="section">
    <h2>🚀 Project Overview</h2>
    <ul>
      <li><span class="highlight">Students:</span> Browse, enroll, and complete courses at their own pace or join sessions.</li>
      <li><span class="highlight">Instructors:</span> Create, manage, and deliver courses with multimedia content.</li>
    </ul>
  </div>

  <div class="section">
    <h2>🛠️ Technology Stack</h2>
    <ul>
      <li>Frontend: Angular 18</li>
      <li>Backend: .NET Core 8 with Entity Framework Core 8</li>
      <li>Database: Azure SQL Server</li>
      <li>Authentication: Azure AD B2C</li>
      <li>Storage: Azure Storage Account (for media files)</li>
      <li>Caching: In-Memory Cache</li>
      <li>Logging & Monitoring: Serilog, Application Insights, Azure Monitor</li>
      <li>Notifications: SendGrid (Email)</li>
      <li>Deployment: Azure Web App</li>
      <li>Serverless Functions: Azure Functions (for specific tasks)</li>
      <li>Secrets Management: Azure Key Vault</li>
      <li>Source Control & CI/CD: Azure DevOps</li>
    </ul>
  </div>

  <div class="section">
    <h2>✨ Key Features</h2>
    <ul>
      <li><span class="highlight">User Management:</span> Secure registration, authentication, and profile management via Azure AD B2C.</li>
      <li><span class="highlight">Course Management:</span> Instructors can create courses with multiple sessions including videos, documents, and other learning materials.</li>
      <li><span class="highlight">Enrollment:</span> Students can enroll in courses, track progress, and receive certificates upon completion.</li>
      <li><span class="highlight">Payment Integration:</span> Secure payment processing for paid courses through a payment gateway.</li>
      <li><span class="highlight">Notifications:</span> Automated email notifications (course updates, payment confirmations, reminders) powered by SendGrid.</li>
      <li><span class="highlight">Content Delivery:</span> Supports both sessions and on-demand learning.</li>
      <li><span class="highlight">Monitoring & Insights:</span> Performance and user behavior analytics via Application Insights.</li>
      <li><span class="highlight">Security:</span> Application secrets and sensitive data stored securely in Azure Key Vault.</li>
      <li><span class="highlight">Scalability:</span> Hosted on Azure Web App, scaling to handle large numbers of users and courses.</li>
      <li><span class="highlight">Automation:</span> Workflow automation using Azure Logic Apps to reduce manual intervention.</li>
    </ul>
  </div>

  <div class="section">
    <h2>💡 Benefits</h2>
    <ul>
      <li><span class="highlight">Scalability:</span> Designed to support high user and course volume.</li>
      <li><span class="highlight">Security:</span> Enterprise-grade security leveraging Azure services.</li>
      <li><span class="highlight">User Experience:</span> Smooth and user-friendly interface for both students and instructors.</li>
      <li><span class="highlight">Flexibility:</span> Supports both live and self-paced learning styles.</li>
      <li><span class="highlight">Automation:</span> Streamlined workflows through integration with Azure services.</li>
    </ul>
  </div>

  <div class="section">
    <h2>⚙️ Deployment</h2>
    <ul>
      <li>Deployed on Azure Web App</li>
      <li>CI/CD pipelines configured with Azure DevOps</li>
      <li>Infrastructure and workflows integrated with Azure Functions, Key Vault, and Logic Apps</li>
    </ul>
  </div>

</body>
</html>
