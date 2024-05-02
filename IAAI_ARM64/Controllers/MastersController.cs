using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ganss.Xss;
using IAAI_ARM64.Data;
using IAAI_ARM64.Models;

namespace IAAI_ARM64.Controllers
{
    public class MastersController : Controller
    {
        private Context db = new Context();

        // GET: Masters
        public ActionResult Index()
        {
            return View(db.Master.ToList());
        }
        public ActionResult IndexFontPage()
        {
            return View(db.Master.ToList());
        }

        // GET: Masters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        public ActionResult DetailsPage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // GET: Masters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Masters/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Name,Position,Photo,Experience,Education,Introduction,Detail")] Master master, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    // 使用 GUID 生成文件名
                    string path = Server.MapPath("~/Upload/");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    //確認路徑存在
                    if (!Directory.Exists(path))
                    {   // 如果不存在，創建資料夾
                        Directory.CreateDirectory(path);
                    }
                    // 保存文件
                    file.SaveAs(Path.Combine(path, fileName));
                    master.Photo = fileName;
                    //設置TempData來傳遞上傳成功的消息
                    TempData["UploadSuccess"] = "檔案上傳成功!";

                }
                else
                {
                    TempData["UploadError"] = "Please select a file to upload.";
                }

                var sanitizer = new HtmlSanitizer();//使用Ganss.Xss套件,防止XSS攻擊
                master.Experience = sanitizer.Sanitize(master.Experience);
                master.Education = sanitizer.Sanitize(master.Education);
                master.Detail = sanitizer.Sanitize(master.Detail);
                master.Introduction = sanitizer.Sanitize(master.Introduction);
                db.Master.Add(master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(master);
        }

        // GET: Masters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // POST: Masters/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Name,Position,Photo,Experience,Education,Introduction,Detail")] Master master, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {


                if (file != null && file.ContentLength > 0)
                {
                    // 使用 GUID 生成文件名
                    string path = Server.MapPath("~/Upload/");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    //確認路徑存在
                    if (!Directory.Exists(path))
                    {   // 如果不存在，創建資料夾
                        Directory.CreateDirectory(path);
                    }
                    // 保存文件
                    file.SaveAs(Path.Combine(path, fileName));
                    master.Photo = fileName;
                    //設置TempData來傳遞上傳成功的消息
                    TempData["UploadSuccess"] = "檔案上傳成功!";

                }
                else
                {
                    ///////////////////////////////////////說明////////////////////////////////////////////////////////////////////////////////////
                    //下面這行代碼是用來從資料庫裡找到一條特定的記錄，但不會追踪這條記錄後續的任何改動。具體來說:                                                //
                    //db.Masters：這表示我們要從資料庫中的Masters表格或集合查詢資料。                                                                  //
                    //.AsNoTracking()：這是告訴Entity Framework，我們查詢出來的資料只是要讀取，不會對其進行修改。這麼做可以提升查詢的效率。                  //
                    //.FirstOrDefault(m => m.Id == master.Id)：這表示我們要從所有資料中找出第一條Id欄位值和當前master物件的Id值相同的記錄。               //
                    //如果沒找到，就會返回null（也就是沒有找到任何符合條件的資料）。總的來說，這行代碼的意思是：在不需要後續修改這條記錄的情況下                 //
                    //從資料庫中查詢出當前正在被編輯的Master記錄的原始資料。如果用戶沒有上傳新的圖片，我們可以用這種方式來保留原本的圖片資料，避免它被刪除或覆蓋。 //
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    
                    // 如果文件未上傳，則保留原有的Photo值
                    if (master.Id != 0) // 確定Id不為0，即確定是編輯模式
                    {   //獲取原有的Master對象
                        //AsNoTracking()方法可以防止EF Core將對象加入到上下文中，這樣EF Core就不會追蹤對象的變化
                        //這樣可以確保Photo值不會被修改
                        var existingMaster = db.Master.AsNoTracking().FirstOrDefault(m => m.Id == master.Id);
                        if (existingMaster != null)
                        {
                            // 保留原有的Photo值
                            master.Photo = existingMaster.Photo;
                        }
                    }
                    else
                    {
                        //處理文件未上傳的情況，例如設置錯誤消息
                        TempData["UploadError"] = "Please select a file to upload.";
                    }
                }





                var sanitizer = new HtmlSanitizer();//使用Ganss.Xss套件,防止XSS攻擊
                master.Experience = sanitizer.Sanitize(master.Experience);
                master.Education = sanitizer.Sanitize(master.Education);
                master.Detail = sanitizer.Sanitize(master.Detail);
                master.Introduction = sanitizer.Sanitize(master.Introduction);
                db.Entry(master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(master);
        }

        // GET: Masters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // POST: Masters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master master = db.Master.Find(id);
            db.Master.Remove(master);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UploadFile()
        {
            return View();
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
