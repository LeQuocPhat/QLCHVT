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
    public class NhapKhoController : Controller
    {
        private QLCHVTEntities db = new QLCHVTEntities();

        // GET: NhapKho
        public ActionResult Index()
        {
            var nhapKhoes = db.NhapKhoes.Include(n => n.Nhanvien);
            return View(nhapKhoes.ToList());
        }

        // GET: NhapKho/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            if (nhapKho == null)
            {
                return HttpNotFound();
            }
            return View(nhapKho);
        }

        // GET: NhapKho/Create
        public ActionResult Create()
        {
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV");
            return View();
        }

        // POST: NhapKho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNK,MaNV,NgayLapPhieu")] NhapKho nhapKho)
        {
            if (ModelState.IsValid)
            {
                db.NhapKhoes.Add(nhapKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", nhapKho.MaNV);
            return View(nhapKho);
        }

        // GET: NhapKho/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            if (nhapKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", nhapKho.MaNV);
            return View(nhapKho);
        }

        // POST: NhapKho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNK,MaNV,NgayLapPhieu")] NhapKho nhapKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhapKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", nhapKho.MaNV);
            return View(nhapKho);
        }

        // GET: NhapKho/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            if (nhapKho == null)
            {
                return HttpNotFound();
            }
            return View(nhapKho);
        }

        // POST: NhapKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            db.NhapKhoes.Remove(nhapKho);
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
