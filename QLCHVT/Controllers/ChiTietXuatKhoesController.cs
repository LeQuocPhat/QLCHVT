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
    public class ChiTietXuatKhoesController : Controller
    {
        private QLCHVTEntities db = new QLCHVTEntities();

        // GET: ChiTietXuatKhoes
        public ActionResult Index()
        {
            var chiTietXuatKhoes = db.ChiTietXuatKhoes.Include(c => c.XuatKho).Include(c => c.VatTu);
            return View(chiTietXuatKhoes.ToList());
        }

        // GET: ChiTietXuatKhoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietXuatKho chiTietXuatKho = db.ChiTietXuatKhoes.Find(id);
            if (chiTietXuatKho == null)
            {
                return HttpNotFound();
            }
            return View(chiTietXuatKho);
        }

        // GET: ChiTietXuatKhoes/Create
        public ActionResult Create()
        {
            ViewBag.MaXK = new SelectList(db.XuatKhoes, "MaXK", "MaKH");
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT");
            return View();
        }

        // POST: ChiTietXuatKhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaXK,MaVT,Soluong,DongiaBan,Giamgia")] ChiTietXuatKho chiTietXuatKho)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietXuatKhoes.Add(chiTietXuatKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaXK = new SelectList(db.XuatKhoes, "MaXK", "MaKH", chiTietXuatKho.MaXK);
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT", chiTietXuatKho.MaVT);
            return View(chiTietXuatKho);
        }

        // GET: ChiTietXuatKhoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietXuatKho chiTietXuatKho = db.ChiTietXuatKhoes.Find(id);
            if (chiTietXuatKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaXK = new SelectList(db.XuatKhoes, "MaXK", "MaKH", chiTietXuatKho.MaXK);
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT", chiTietXuatKho.MaVT);
            return View(chiTietXuatKho);
        }

        // POST: ChiTietXuatKhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaXK,MaVT,Soluong,DongiaBan,Giamgia")] ChiTietXuatKho chiTietXuatKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietXuatKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaXK = new SelectList(db.XuatKhoes, "MaXK", "MaKH", chiTietXuatKho.MaXK);
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT", chiTietXuatKho.MaVT);
            return View(chiTietXuatKho);
        }

        // GET: ChiTietXuatKhoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietXuatKho chiTietXuatKho = db.ChiTietXuatKhoes.Find(id);
            if (chiTietXuatKho == null)
            {
                return HttpNotFound();
            }
            return View(chiTietXuatKho);
        }

        // POST: ChiTietXuatKhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChiTietXuatKho chiTietXuatKho = db.ChiTietXuatKhoes.Find(id);
            db.ChiTietXuatKhoes.Remove(chiTietXuatKho);
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
