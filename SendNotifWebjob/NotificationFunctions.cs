using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendNotifWebjob
{
    public class NotificationFunctions
    {
        [NoAutomaticTrigger]
        public void SendNotifications(TextWriter log)
        {
            log.WriteLine("SendNotifications Logic");
        }
    }
}
