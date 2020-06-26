using System.Data.Entity;
using BeautyProdsEF;
using MailKit.Net.Smtp;
using Microsoft.Azure.WebJobs;
using MimeKit;
using SendNotifWebJob.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SendNotifWebjob
{
    public class NotificationFunctions
    {
        private DataManager dm = new DataManager();
            
        [NoAutomaticTrigger]
        public void SendNotifications(TextWriter log)
        {
            List<BottleRequest> brNotifications = null;
            try
            {
                //Read the table 
                log.WriteLine("Starts SendNotifications Functions calls");
                using (var context = new BeautyProdsEntities())
                {
                    brNotifications = context.BottleRequests
                        .Where(br => br.SendNotification == 1
                                     && br.NotificationSent == false)
                        .Include("Vendor")
                        .Include("Bottle")
                        .ToList();
                }

                foreach (var notification in brNotifications)
                {
                    if(DateTime.Now.Date.AddDays(5) == notification.DueDate)
                    {
                        SendEmail(notification);
                    }
                    
                }

                // var allNotifications = dm.getList<BottleRequest>();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendEmail( BottleRequest notificationInfo)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            //var resourcesNames = assembly.GetManifestResourceNames();
            //string resourceName = resourcesNames.FirstOrDefault(str => str.EndsWith("BRNotifTemplate.html"));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BeautyProds Corp.", "marcelorangelsanchez@hotmail.com"));
            message.To.Add(new MailboxAddress(notificationInfo.Vendor.Company, notificationInfo.Vendor.ContactEmail));
            message.Subject = "Bottles Supply Requirement, Due Date: " + notificationInfo.DueDate.ToShortDateString();
            var builder = new BodyBuilder();
            StringBuilder templateText = new StringBuilder(File.ReadAllText("Templates/BRNotifTemplate.html"));
            templateText.Replace("$$CurrentDate$$", DateTime.Now.ToShortDateString());
            templateText.Replace("$$CompanyName$$", notificationInfo.Vendor.Company);
            templateText.Replace("$$BottleQty$$", notificationInfo.ReqQuantity.ToString());
            templateText.Replace("$$BottleName$$", notificationInfo.Bottle.Name);
            templateText.Replace("$$DueDate$$", notificationInfo.DueDate.ToShortDateString());

            builder.HtmlBody = templateText.ToString();

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.live.com", 587, false);
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("marcelorangelsanchez@hotmail.com", "");
                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}
