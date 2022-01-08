using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using QLCHVT.Models;


namespace QLCHVT.Controllers
{
    public class formNKController : Controller
    {
        private QLCHVTEntities db = new QLCHVTEntities();
            // GET: formNK
        public ActionResult Index()
        {
            var nhapKhoes = db.NhapKhoes.Include(n => n.Nhanvien);
            return View(nhapKhoes.ToList());
        }

        public ActionResult AddOrder()
        {
            List<VatTu> vattu = db.VatTus.ToList();
            List<Nhanvien> nhanvien = db.Nhanviens.ToList();

            // Tạo SelectList
            SelectList cateList = new SelectList(vattu, "MaVT", "TenVT");
            SelectList nhanVien = new SelectList(nhanvien, "MaNV", "Ten");

            // Set vào ViewBag
            ViewBag.VatTu = cateList;
            ViewBag.NhanVien = nhanVien;
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder([Bind(Include = "MaNK,MaVT,Soluong,DonGiaNhap")] ChiTietNhapKho CTNK,
            [Bind(Include = "MaNK,MaNV,NgayLapPhieu")] NhapKho NK, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var Mavalid = db.NhapKhoes.Where(x => x.MaNK == form["MaNK"]);
                if(Mavalid == null)
                {
                    NK.MaNK = form["MaNK"];
                    NK.MaNV = int.Parse(form["NhanVien"]);
                    db.NhapKhoes.Add(NK);
                }
                CTNK.MaVT = form["TenVT"];
                db.ChiTietNhapKhoes.Add(CTNK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}