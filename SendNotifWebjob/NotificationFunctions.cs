using MailKit.Net.Smtp;
using Microsoft.Azure.WebJobs;
using MimeKit;
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
            log.WriteLine("Starts SendNotifications Functions calls");
            SendEmail();
        }

        public void SendEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Marcelo", "marcelorangelsanchez@hotmail.com"));
            message.To.Add(new MailboxAddress("Marce Recipient", "marcelorangelsanchez@gmail.com"));
            message.Subject = "Test Message";
            var builder = new BodyBuilder();
            builder.HtmlBody = string.Format(@"<p>Hey Alice,<br>
            <p>What are you up to this weekend? Monica is throwing one of her parties on
            Saturday and I was hoping you could make it.<br>
            <p>Will you be my +1?<br>
            <p>-- Joey<br>
<br/><br/>
<table>
  <tr>
    <th>Company</th>
    <th>Contact</th>
    <th>Country</th>
  </tr>
  <tr>
    <td>Alfreds Futterkiste</td>
    <td>Maria Anders</td>
    <td>Germany</td>
  </tr>
  <tr>
    <td>Centro comercial Moctezuma</td>
    <td>Francisco Chang</td>
    <td>Mexico</td>
  </tr>
  <tr>
    <td>Ernst Handel</td>
    <td>Roland Mendel</td>
    <td>Austria</td>
  </tr>
  <tr>
    <td>Island Trading</td>
    <td>Helen Bennett</td>
    <td>UK</td>
  </tr>
  <tr>
    <td>Laughing Bacchus Winecellars</td>
    <td>Yoshi Tannamuri</td>
    <td>Canada</td>
  </tr>
  <tr>
    <td>Magazzini Alimentari Riuniti</td>
    <td>Giovanni Rovelli</td>
    <td>Italy</td>
  </tr>
</table>
                    ");
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
