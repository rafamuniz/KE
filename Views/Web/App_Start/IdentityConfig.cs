﻿using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.Models;
using KarmicEnergy.Web.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KarmicEnergy.Web
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configAWSSESAsync((EmailMessage)message);
        }

        public async Task SendAsync(EmailMessage message)
        {
            // Plug in your email service here to send an email.
            await configAWSSESAsync(message);
        }

        private async Task configAWSSESAsync(EmailMessage emailMessage)
        {
            String from = ConfigurationManager.AppSettings["EmailService:From"];
            String smtpServer = ConfigurationManager.AppSettings["EmailService:SMTPServer"];
            Int32 smtpPort = Int32.Parse(ConfigurationManager.AppSettings["EmailService:SMTPPort"].ToString());
            String smtpUsername = ConfigurationManager.AppSettings["EmailService:SMTPUsername"];
            String smtpPassword = ConfigurationManager.AppSettings["EmailService:SMTPPassword"];

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from, "KE Siteminder");
            mailMessage.Subject = emailMessage.Subject;
            mailMessage.Body = emailMessage.Body;
            mailMessage.IsBodyHtml = true;

            MailAddress mailTo = new MailAddress(emailMessage.Destination, emailMessage.DestinationName);
            mailMessage.To.Add(mailTo);

            // Create an SMTP client with the specified host name and port.
            using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
            {
                // Create a network credential with your SMTP user name and password.
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                // Use SSL when accessing Amazon SES. The SMTP session will begin on an unencrypted connection, and then 
                // the client will issue a STARTTLS command to upgrade to an encrypted connection using SSL.
                client.EnableSsl = true;

                // Send the email. 
                try
                {
                    //client.Send(from, emailMessage.Destination, emailMessage.Subject, emailMessage.Body);
                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private async Task configSendGridasync(EmailMessage message)
        {
            //var sendGridMessage = new SendGridMessage();

            //sendGridMessage.AddTo(message.Destination);
            //sendGridMessage.From = new MailAddress(ConfigurationManager.AppSettings["EmailService:From"], "Karmic Energy - Support");
            //sendGridMessage.Subject = message.Subject;
            //sendGridMessage.Text = message.Body;
            //sendGridMessage.Html = message.Body;
            //sendGridMessage.EnableTemplateEngine(message.TemplateId);

            String apiKey = ConfigurationManager.AppSettings["EmailService:APIKey"];

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailService:Account"],
                                                    ConfigurationManager.AppSettings["EmailService:Password"]);

            // Create a Web transport for sending email.
            //var transportWeb = new SendGrid.Web(credentials);
            //var transportWeb = new SendGrid.Web(apiKey);

            try
            {
                //// Send the email.
                //if (transportWeb != null)
                //{
                //    await transportWeb.DeliverAsync(sendGridMessage);
                //}
                //else
                //{
                //    //Trace.TraceError("Failed to create Web transport.");
                //    await Task.FromResult(0);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });

            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store)
            : base(store)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<ApplicationContext>());
            return new ApplicationRoleManager(roleStore);
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
