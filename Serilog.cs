using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Serilog;
using Serilog.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Active_Active
{
    

    class Serilog
    {
        [Test]

        public void Azuelogin()
        {
           /*MailMessage mail = new MailMessage();
            String Fromemail = "adeo@galaxe.com";
            String password = "Galaxy@852";
            String ToEmail = "adeo@galaxe.com";
            String Subject = "Execution Report";
            String contentBody = "<h3>This is the execution report for today</h3>";

            mail.From = new MailAddress(Fromemail);
            mail.To.Add(ToEmail);
            mail.Subject = Subject;
            mail.Body = contentBody;
            mail.IsBodyHtml = true;
            mail.Attachments.Add(new Attachment(@"D:\Project\Active-Active\ExtentReports\index.html"));

            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);

            smtp.Credentials = new NetworkCredential(Fromemail, password);
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }*/



        }

    }
   
   
}
