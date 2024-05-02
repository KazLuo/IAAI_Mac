using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using IAAI_ARM64.Data;
using IAAI_ARM64.Models;

namespace IAAI_ARM64.Controllers
{
    public class MembersController : Controller
    {
        private Context db = new Context();

        // GET: Members
        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,Name,Gender,Birthday,ApplyType,Address,Email,International,CurrentPosition,Title,Education,CreateTime")] Members members)
        {
            if (ModelState.IsValid)
            {
                
                db.Members.Add(members);
                members.CreateTime = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(members);
        }
        public ActionResult RegisterAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAccount(Register_VM register)
        {

            if (!this.IsCaptchaValid("驗證碼錯誤！"))
            {
                @ViewBag.ErrMessage = "驗證碼錯誤！";
                return View(register);
            }
            if(register.Members.Gender == 0)
            {
                ViewBag.GenderErr = "請選擇性別";
                return View(register);
            }
            if (ModelState.IsValid)
            {

                db.Members.Add(register.Members);
                register.Members.CreateTime = DateTime.Now;
                if(register.ServiceHistory!= null)
                {
                    db.ServiceHistory.Add(register.ServiceHistory);
                    register.Members.CreateTime = DateTime.Now;
                }
                db.SaveChanges();
                return RedirectToAction("RegisterAccount");
            }

            return View(register);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,Name,Gender,Birthday,ApplyType,Address,Email,International,CurrentPosition,Title,Education,CreateTime")] Members members)
        {
            if (ModelState.IsValid)
            {
                db.Entry(members).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(members);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Members members = db.Members.Find(id);
            db.Members.Remove(members);
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
    }
}
