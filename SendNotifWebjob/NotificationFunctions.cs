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
                DateTime SystemDueDate = DateTime.Now.Date.AddDays(5);
                //Read the table 
                log.WriteLine("Starts SendNotifications Functions calls, System Due Date: " + SystemDueDate);
                using (var context = new BeautyProdsEntities())
                {
                    brNotifications = context.BottleRequests
                        .Where(br => br.SendNotification == 1
                                     && br.NotificationSent == false
                                     && SystemDueDate == br.DueDate)
                        .Include("Vendor")
                        .Include("Bottle")
                        .ToList();
                
                    log.WriteLine("Found " + brNotifications.Count + "Notifications to be sent");
                    foreach (var notification in brNotifications)
                    {

                        log.WriteLine("Sending notification to: " + notification.Vendor.Company + ", " + notification.ReqQuantity+ " Bottles");
                        SendEmail(notification, log);
                        log.WriteLine("Notification Sent!");
                        notification.NotificationSent = true;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLine("Exception on SendNotifications Function: " + ex.Message);
                throw ex;
            }
        }

        public void SendEmail( BottleRequest notificationInfo, TextWriter log)
        {
            try
            {
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
            catch (Exception ex)
            {
                log.WriteLine("Exception on SendEmail Method: " + ex.Message);
                throw ex;
            }
        }
    }
}
