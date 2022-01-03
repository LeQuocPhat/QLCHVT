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
    public class VatTuController : Controller
    {
        private QLCHVTEntities db = new QLCHVTEntities();

        // GET: VatTu
        public ActionResult Index()
        {
            var vatTus = db.VatTus.Include(v => v.LoaiVatTu);
            return View(vatTus.ToList());
        }

        // GET: VatTu/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatTu vatTu = db.VatTus.Find(id);
            if (vatTu == null)
            {
                return HttpNotFound();
            }
            return View(vatTu);
        }

        // GET: VatTu/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiVT = new SelectList(db.LoaiVatTus, "MaLoaiVT", "TenLoaiVT");
            return View();
        }

        // POST: VatTu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaVT,TenVT,Donvitinh,Dongia,MaLoaiVT,HinhVT")] VatTu vatTu)
        {
            if (ModelState.IsValid)
            {
                db.VatTus.Add(vatTu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiVT = new SelectList(db.LoaiVatTus, "MaLoaiVT", "TenLoaiVT", vatTu.MaLoaiVT);
            return View(vatTu);
        }

        // GET: VatTu/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatTu vatTu = db.VatTus.Find(id);
            if (vatTu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiVT = new SelectList(db.LoaiVatTus, "MaLoaiVT", "TenLoaiVT", vatTu.MaLoaiVT);
            return View(vatTu);
        }

        // POST: VatTu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVT,TenVT,Donvitinh,Dongia,MaLoaiVT,HinhVT")] VatTu vatTu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vatTu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiVT = new SelectList(db.LoaiVatTus, "MaLoaiVT", "TenLoaiVT", vatTu.MaLoaiVT);
            return View(vatTu);
        }

        // GET: VatTu/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatTu vatTu = db.VatTus.Find(id);
            if (vatTu == null)
            {
                return HttpNotFound();
            }
            return View(vatTu);
        }

        // POST: VatTu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VatTu vatTu = db.VatTus.Find(id);
            db.VatTus.Remove(vatTu);
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
