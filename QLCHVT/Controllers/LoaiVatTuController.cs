using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHVT.Models;

namespace QLCHVT.Controllers
{
    public class LoaiVatTuController : Controller   
    {
        private QLCHVTEntities db = new QLCHVTEntities();

        // GET: LoaiVatTu
        public ActionResult Index(string SearchString = "")
        {
            if (SearchString != "")
            {
                var loaivatTus = db.LoaiVatTus.Where(
                x => x.TenLoaiVT.ToUpper().Contains(SearchString.ToUpper()));
                return View(loaivatTus.ToList());
            }       
            else
            {
                return View(db.LoaiVatTus.ToList());
            }
        }

        // GET: LoaiVatTu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVatTu loaiVatTu = db.LoaiVatTus.Find(id);
            if (loaiVatTu == null)
            {
                return HttpNotFound();
            }
            return View(loaiVatTu);
        }

        // GET: LoaiVatTu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiVatTu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiVT,TenLoaiVT")] LoaiVatTu loaiVatTu)
        {
            if (ModelState.IsValid)
            {
                db.LoaiVatTus.Add(loaiVatTu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiVatTu);
        }

        // GET: LoaiVatTu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVatTu loaiVatTu = db.LoaiVatTus.Find(id);
            if (loaiVatTu == null)
            {
                return HttpNotFound();
            }
            return View(loaiVatTu);
        }

        // POST: LoaiVatTu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiVT,TenLoaiVT")] LoaiVatTu loaiVatTu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiVatTu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiVatTu);
        }

        // GET: LoaiVatTu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVatTu loaiVatTu = db.LoaiVatTus.Find(id);
            if (loaiVatTu == null)
            {
                return HttpNotFound();
            }
            return View(loaiVatTu);
        }

        // POST: LoaiVatTu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiVatTu loaiVatTu = db.LoaiVatTus.Find(id);
            db.LoaiVatTus.Remove(loaiVatTu);
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
