using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalSite1.UI.MVC.Models;
using System.Net;
using System.Net.Mail;

namespace PersonalSite1.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PortfolioDetails()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel cvm)
        {
            string body = $"{cvm.Name} has sent you the following message: <br />" + $"{cvm.Message} <strong>from the email address:</strong> {cvm.Email}";

            MailMessage m = new MailMessage("admin@davidmuolo.com", "davidmuolo1@gmail.com", cvm.Subject, body);

            m.IsBodyHtml = true;
            m.Priority = MailPriority.High;
            m.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient("mail.davidmuolo.com");
            client.Credentials = new NetworkCredential("admin@davidmuolo.com", "Stubble848!");

            client.Port = 8889;
            try
            {
                client.Send(m);
            }
            catch(Exception e)
            {
                ViewBag.Message = e.StackTrace;
            }
            return Json(cvm);
        }
    }
}