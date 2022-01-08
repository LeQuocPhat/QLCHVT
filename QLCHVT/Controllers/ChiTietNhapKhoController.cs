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
    public class ChiTietNhapKhoController : Controller
    {
        private QLCHVTEntities db = new QLCHVTEntities();

        // GET: ChiTietNhapKho
        public ActionResult Index()
        {
            var chiTietNhapKhoes = db.ChiTietNhapKhoes.Include(c => c.NhapKho).Include(c => c.VatTu);
            return View(chiTietNhapKhoes.ToList());
        }

        // GET: ChiTietNhapKho/Details/5
        public ActionResult Details(string id)
        {

            List<ChiTietNhapKho> ctnk = db.ChiTietNhapKhoes.ToList();
            List<NhapKho> nhapkho = db.NhapKhoes.ToList();
            List<VatTu> vattu = db.VatTus.ToList();
            List<Nhanvien> nhanvien = db.Nhanviens.ToList();
            var main = from h in nhapkho
                       join k in nhanvien on h.MaNV equals k.MaNV
                       where (h.MaNK == id)
                       select new ViewModel
                       {
                           nhapkho = h, /*dịnh nghĩa ở viewmodel*/
                           nhanvien = k,
                       };
            var sub = from c in ctnk
                      join s in vattu on c.MaVT equals s.MaVT
                      where (c.MaNK == id)
                      select new ViewModel
                      {
                          ctnk = c,
                          vattu = s,
                          Thanhtien = Convert.ToDouble(c.DongiaNhap * c.Soluong)
                      };
            ViewBag.Main = main;
            ViewBag.Sub = sub;
            return View();


        }

        // GET: ChiTietNhapKho/Create
        public ActionResult Create()
        {
            ViewBag.MaNK = new SelectList(db.NhapKhoes, "MaNK", "MaNK");
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT");
            return View();
        }

        // POST: ChiTietNhapKho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNK,MaVT,Soluong,DongiaNhap")] ChiTietNhapKho chiTietNhapKho)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietNhapKhoes.Add(chiTietNhapKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNK = new SelectList(db.NhapKhoes, "MaNK", "MaNK", chiTietNhapKho.MaNK);
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT", chiTietNhapKho.MaVT);
            return View(chiTietNhapKho);
        }

        // GET: ChiTietNhapKho/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietNhapKho chiTietNhapKho = db.ChiTietNhapKhoes.Find(id);
            if (chiTietNhapKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNK = new SelectList(db.NhapKhoes, "MaNK", "MaNK", chiTietNhapKho.MaNK);
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT", chiTietNhapKho.MaVT);
            return View(chiTietNhapKho);
        }

        // POST: ChiTietNhapKho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNK,MaVT,Soluong,DongiaNhap")] ChiTietNhapKho chiTietNhapKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietNhapKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNK = new SelectList(db.NhapKhoes, "MaNK", "MaNK", chiTietNhapKho.MaNK);
            ViewBag.MaVT = new SelectList(db.VatTus, "MaVT", "TenVT", chiTietNhapKho.MaVT);
            return View(chiTietNhapKho);
        }

        // GET: ChiTietNhapKho/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietNhapKho chiTietNhapKho = db.ChiTietNhapKhoes.Find(id);
            if (chiTietNhapKho == null)
            {
                return HttpNotFound();
            }
            return View(chiTietNhapKho);
        }

        // POST: ChiTietNhapKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChiTietNhapKho chiTietNhapKho = db.ChiTietNhapKhoes.Find(id);
            db.ChiTietNhapKhoes.Remove(chiTietNhapKho);
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
