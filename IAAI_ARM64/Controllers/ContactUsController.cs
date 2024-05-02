using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI_ARM64.Data;
using IAAI_ARM64.Models;
using CaptchaMvc.HtmlHelpers;
using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using System.Web.ModelBinding;
using System.Configuration;


namespace IAAI_ARM64.Controllers
{
    public class ContactUsController : Controller
    {
        private Context db = new Context();

        // GET: ContactUs
        public ActionResult Index()
        {
            return View(db.ContactUs.ToList());
        }


        public ActionResult ContactUsDBoard(string sortOrder, string searchString)
        {
            ViewBag.ShowSearchBar = true;
            ViewBag.SearchAction = "ContactUsDBoard";
            ViewBag.SearchController = "ContactUs";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var contactMember = from s in db.ContactUs
                                select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                contactMember = contactMember.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    contactMember = contactMember.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    contactMember = contactMember.OrderBy(s => s.CreateTime);
                    break;
                case "date_desc":
                    contactMember = contactMember.OrderByDescending(s => s.CreateTime);
                    break;
                default:
                    contactMember = contactMember.OrderBy(s => s.Name);
                    break;
            }




            return View(contactMember.ToList());
        }

        public ActionResult IndexFontPage()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndexFontPage([Bind(Include = "Id,Name,Gender,Email,Phone,Message,CreateTime")] ContactUs contactUs)
        {



            if (!this.IsCaptchaValid("驗證碼錯誤！"))
            {
                ViewBag.ErrMessage = "驗證碼錯誤！";
                return View(contactUs);
            }



            // 然后检查整个模型的状态
            if (ModelState.IsValid)
            {
                db.ContactUs.Add(contactUs);
                db.SaveChanges();
                SendGmail(contactUs);
                TempData["SuccessMessage"] = "感謝您的留言！";
                return RedirectToAction("IndexFontPage");
            }


            return View(contactUs);

        }

        // GET: ContactUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        // GET: ContactUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Gender,Email,Phone,Message,CreateTime")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                db.ContactUs.Add(contactUs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactUs);
        }

        // GET: ContactUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        // POST: ContactUs/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,Email,Phone,Message,CreateTime")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactUs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactUs);
        }

        // GET: ContactUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactUs contactUs = db.ContactUs.Find(id);
            db.ContactUs.Remove(contactUs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void SendGmail(ContactUs contactUs)
        {
            string STMPKey = ConfigurationManager.AppSettings["gmailSTMPKEY"];
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("IAAI", "acmailsend.01@gmail.com"));
            message.To.Add(new MailboxAddress(contactUs.Name, contactUs.Email));
            message.Cc.Add(new MailboxAddress("Admin", "admin-email@gmail.com"));
            message.Subject = "Thank You for Contacting Us";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<h1>Thank you for contacting us, {contactUs.Name}!</h1>" +
                           $"<p>We have received your message and will get back to you as soon as possible.</p>" +
                           $"<h3>Your Details</h3>" +
                           $"<p><strong>Email:</strong> {contactUs.Email}</p>" +
                           $"<p><strong>Phone:</strong> {contactUs.Phone}</p>" +
                           $"<p><strong>Message:</strong> {contactUs.Message}</p>"
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("acmailsend.01@gmail.com", STMPKey);
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}
