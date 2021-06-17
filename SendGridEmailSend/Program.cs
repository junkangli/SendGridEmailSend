using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SendGridEmailSend
{
    class Program
    {
        static SendGridSettings settings = new();
        static EmailSendSettings emailSettings = new();

        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var sendGrid = new SendGridClient(settings.ApiKey);

            var from = new EmailAddress(emailSettings.FromEmailAddress, emailSettings.FromEmailName);
            var subject = emailSettings.Subject;
            var to = new EmailAddress(emailSettings.ToEmailAddress, emailSettings.ToEmailName);
            var content = $"This email is sent using SendGrid subuser {settings.SubUser}.";
            var plainTextContent = content;
            var htmlContent = content;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await sendGrid.SendEmailAsync(msg);
            Console.WriteLine("Send {0}", (response.StatusCode == HttpStatusCode.Accepted) ? "SUCCESS" : "FAIL");
            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    IConfigurationRoot configurationRoot = configuration.Build();
                    configurationRoot.GetSection(nameof(SendGridSettings))
                                     .Bind(settings);
                    configurationRoot.GetSection(nameof(EmailSendSettings))
                                     .Bind(emailSettings);
                });
    }
}
