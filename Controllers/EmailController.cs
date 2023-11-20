using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AutomobileInsuranceManagement_AIM.Controllers
{
    public class EmailController : Controller
    {
        public ActionResult Index()
        {
            string gmail = "20bsca151yaminipriyaj@skacas.ac.in";
            string to = "priyadarshanmanoharan@gmail.com";
            using (MailMessage mail=new MailMessage(gmail, to))
            {
                mail.Subject = "One small Step to man";
                mail.Body = "Skyler,I have won";

                mail.IsBodyHtml = false;

                using (SmtpClient smtp=new SmtpClient())
                {
                    smtp.Host= "smtp.gmail.com";
                    smtp.EnableSsl= true;
                    NetworkCredential nc = new NetworkCredential(gmail," ");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }
            }
                return Content("sent");
        }
    }
}