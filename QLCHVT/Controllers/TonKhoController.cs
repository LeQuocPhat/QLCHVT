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
    public class TonKhoController : Controller
    {
        private QLCHVTEntities db = new QLCHVTEntities();

        // GET: TonKho
        public ActionResult Index()
        {
            var tonKhoes = db.TonKhoes.Include(t => t.VatTu);
            return View(tonKhoes.ToList());
        }

        // GET: TonKho/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TonKho tonKho = db.TonKhoes.Find(id);
            if (tonKho == null)
            {
                return HttpNotFound();
            }
            return View(tonKho);
        }

        // GET: TonKho/Create
        public ActionResult Create()
        {
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT");
            return View();
        }

        // POST: TonKho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaVT,NgayLap,GiaNhap,GiaXuat,SLNhap,SLXuat,SLCuoi")] TonKho tonKho)
        {
            if (ModelState.IsValid)
            {
                db.TonKhoes.Add(tonKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT", tonKho.MaVT);
            return View(tonKho);
        }

        // GET: TonKho/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TonKho tonKho = db.TonKhoes.Find(id);
            if (tonKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT", tonKho.MaVT);
            return View(tonKho);
        }

        // POST: TonKho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVT,NgayLap,GiaNhap,GiaXuat,SLNhap,SLXuat,SLCuoi")] TonKho tonKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tonKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT", tonKho.MaVT);
            return View(tonKho);
        }

        // GET: TonKho/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TonKho tonKho = db.TonKhoes.Find(id);
            if (tonKho == null)
            {
                return HttpNotFound();
            }
            return View(tonKho);
        }

        // POST: TonKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TonKho tonKho = db.TonKhoes.Find(id);
            db.TonKhoes.Remove(tonKho);
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
