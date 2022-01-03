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
    public class XuatKhoController : Controller
    {
        private QLCHVTEntities db = new QLCHVTEntities();

        // GET: XuatKho
        public ActionResult Index()
        {
            var xuatKhoes = db.XuatKhoes.Include(x => x.Nhanvien);
            return View(xuatKhoes.ToList());
        }

        // GET: XuatKho/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatKho xuatKho = db.XuatKhoes.Find(id);
            if (xuatKho == null)
            {
                return HttpNotFound();
            }
            return View(xuatKho);
        }

        // GET: XuatKho/Create
        public ActionResult Create()
        {
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV");
            return View();
        }

        // POST: XuatKho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaXK,MaKH,MaNV,NgayLapPhieu,NgayGiaoHang")] XuatKho xuatKho)
        {
            if (ModelState.IsValid)
            {
                db.XuatKhoes.Add(xuatKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", xuatKho.MaNV);
            return View(xuatKho);
        }

        // GET: XuatKho/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatKho xuatKho = db.XuatKhoes.Find(id);
            if (xuatKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", xuatKho.MaNV);
            return View(xuatKho);
        }

        // POST: XuatKho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaXK,MaKH,MaNV,NgayLapPhieu,NgayGiaoHang")] XuatKho xuatKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xuatKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", xuatKho.MaNV);
            return View(xuatKho);
        }

        // GET: XuatKho/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatKho xuatKho = db.XuatKhoes.Find(id);
            if (xuatKho == null)
            {
                return HttpNotFound();
            }
            return View(xuatKho);
        }

        // POST: XuatKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            XuatKho xuatKho = db.XuatKhoes.Find(id);
            db.XuatKhoes.Remove(xuatKho);
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
