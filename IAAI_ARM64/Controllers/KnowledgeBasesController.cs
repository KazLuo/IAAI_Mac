using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI_ARM64.Data;
using IAAI_ARM64.Models;

namespace IAAI_ARM64.Controllers
{
    public class KnowledgeBasesController : Controller
    {
        private Context db = new Context();

        // GET: KnowledgeBases
        public ActionResult Index()
        {
            return View(db.KnowledgeBase.ToList());
        }

        public ActionResult IndexFontPage()
        {
            return View(db.KnowledgeBase.ToList());
        }

        // GET: KnowledgeBases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgeBase knowledgeBase = db.KnowledgeBase.Find(id);
            if (knowledgeBase == null)
            {
                return HttpNotFound();
            }
            return View(knowledgeBase);
        }

        // GET: KnowledgeBases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KnowledgeBases/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InitDate,Title,Download")] KnowledgeBase knowledgeBase, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string path = Server.MapPath("~/Upload/");
                    string fileName = Path.GetFileName(file.FileName);
                    if (!Directory.Exists(path))
                    {   // 如果不存在，創建資料夾
                        Directory.CreateDirectory(path);
                    }
                    // 保存文件
                    file.SaveAs(Path.Combine(path, fileName));
                    knowledgeBase.Download = fileName;
                    TempData["UploadSuccess"] = "檔案上傳成功!";
                    db.KnowledgeBase.Add(knowledgeBase);
                    db.SaveChanges();
                }
                else
                {
                    TempData["UploadError"] = "檔案上傳失敗!";
                }


                return RedirectToAction("Index");
            }

            return View(knowledgeBase);
        }

        // GET: KnowledgeBases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgeBase knowledgeBase = db.KnowledgeBase.Find(id);
            if (knowledgeBase == null)
            {
                return HttpNotFound();
            }
            return View(knowledgeBase);
        }

        // POST: KnowledgeBases/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InitDate,Title,Download")] KnowledgeBase knowledgeBase, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string path = Server.MapPath("~/Upload/");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(Path.Combine(path, fileName));
                    knowledgeBase.Download = fileName;
                    TempData["UploadSuccess"] = "檔案上傳成功!";
                }
                else
                {
                    if (knowledgeBase.Id != 0)
                    {
                        var existingKnowledge = db.KnowledgeBase.AsNoTracking().FirstOrDefault(k => k.Id == knowledgeBase.Id);
                        if(existingKnowledge != null)
                        {
                            knowledgeBase.Download = existingKnowledge.Download;
                        }
                    }
                    else
                    {
                        //處理文件未上傳的情況，例如設置錯誤消息
                        TempData["UploadError"] = "Please select a file to upload.";
                    }
                }
                db.Entry(knowledgeBase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(knowledgeBase);
        }

        // GET: KnowledgeBases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgeBase knowledgeBase = db.KnowledgeBase.Find(id);
            if (knowledgeBase == null)
            {
                return HttpNotFound();
            }
            return View(knowledgeBase);
        }

        // POST: KnowledgeBases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KnowledgeBase knowledgeBase = db.KnowledgeBase.Find(id);
            db.KnowledgeBase.Remove(knowledgeBase);
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
