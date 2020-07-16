using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using TechTalk.SpecFlow;
using System.Threading;
using NUnit.Framework;
using System.IO;
using System.Diagnostics;
using static Io.Cucumber.Messages.GherkinDocument.Types.Feature.Types;
using Serilog;
using System.Configuration;
using System.Xml.Linq;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace Active_Active
{
    [Binding]
    public class Hooks 
    {
        //Global Variable for Extend report
        
        public ExtentTest test;
        public ExtentReports extent;             

        public Hooks()
        {
            test = null;
            extent = null;        

        }
        

        public String getencrypedpassword(String encodedpasssword)
        {
            var encoded_password = Convert.FromBase64String(encodedpasssword);
            string decodedpassword = Encoding.UTF8.GetString(encoded_password);
            return decodedpassword;
        }
        public void initilizelogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("" + loadconfig("LogLocation") + "", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public void logger(String text)
        {
            Log.Information(text);
        }

        public void closelogger()
        {
            Log.CloseAndFlush();
        }

        public String loadconfig(String configkey)
        {

         return ConfigurationManager.AppSettings[configkey];

        }


        [SetUp]
        public void ExtentStart()
        {
            extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(@"" + loadconfig("ReportLocation") + "");
            extent.AttachReporter(htmlReporter);
        }

        [TearDown]
        public void ExtentClose()
        {
            extent.Flush();
            Thread.Sleep(20000);
        }

        public void addtexttoextentreport(String scenario, String text)
        {
            test = extent.CreateTest(scenario).Info(text);
        }

        public void addreportstatus(String text)
        {
            test.Log(Status.Info, text);
            
        }

        public void failstatusreport(String text)
        {
            test.Log(Status.Fail, text);

        }

        public void pasststausreport(String text)
        {
            test.Log(Status.Pass, text);
        }

        public void email()
        {
            MailMessage mail = new MailMessage();
            String Fromemail = "TestProjectReporting@outlook.com";
            String password = loadconfig("email_password");
            String ToEmail = loadconfig("email_To");
            String Subject = "Api Execution Report_Active-Active for : " +DateTime.Now;
            String contentBody = "<h3>Hi All," +
                                            "<br>  &nbsp;" +
                                            "<br>  &nbsp;" +
                                            "<br>Please Find the attached execution report for today." +
                                            "<br>  &nbsp;" +
                                            "<br>  &nbsp;" +
                                            "<br>Thanks & Regards" +
                                            "<br>QA Automation Team" +
                                            "<br>Active-Active</h3>";
            
            mail.From = new MailAddress(Fromemail);
            mail.To.Add(ToEmail);
            mail.Subject = Subject;
            mail.Body = contentBody;
            mail.IsBodyHtml = true;
            mail.Attachments.Add(new Attachment(@""+loadconfig("ReportLocation") +""));
            mail.Attachments.Add(new Attachment(@"" + loadconfig("ReportLocation") + ""));
            // SmtpClient smtp = new SmtpClient("galaxe-com.mail.protection.outlook.com", 587);
            SmtpClient smtp = new SmtpClient("smtp.live.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Fromemail, password);
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }
    }
}
