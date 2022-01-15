using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Mvc;
using QLCHVT.Models;

namespace QLCHVT.Controllers
{
    public class ThongKeController : Controller
    {
        QLCHVTEntities db = new QLCHVTEntities();
        // GET: ThongKe
        public ActionResult Index()
        {
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();
            ViewBag.TongVon = ThongKeTongVon();
            ViewBag.DonHang = ThongKeDonHang();
            ViewBag.NhapKho = ThongKeNhapKho();
            ViewBag.Loai = ThongKeLoaiVT();
            ViewBag.VT = ThongKeVT();
            ViewBag.Tong = Session["Tong"]; 
            //ViewBag.Tong = ThongKeDoanhThuTheoThangNam(
            //    thang, nam);
            return View();
        }

        public double ThongKeDonHang()
        {
            double ddh = db.XuatKhoes.Count();
            return ddh;
        }

        public double ThongKeLoaiVT()
        {
            double Loai = db.LoaiVatTus.Count();
            return Loai;
        }

        public double ThongKeVT()
        {
            double VT = db.VatTus.Count();
            return VT;
        }

        public double ThongKeNhapKho()
        {
            double NhapKho = db.NhapKhoes.Count();
            return NhapKho;
        }

        [HttpPost]
        public RedirectToRouteResult Report(FormCollection form)
        {
            DateTime mont = DateTime.Parse(form["month"]);
            int thang = mont.Month;
            DateTime yea = DateTime.Parse(form["month"]);
            int nam = yea.Year;

            var list = db.XuatKhoes.Where(n => n.NgayLapPhieu.Month == thang && n.NgayLapPhieu.Year == nam);
            decimal Tong = 0;
            foreach (var item in list)
            {
                Tong += decimal.Parse(item.ChiTietXuatKhoes.Sum(n => n.Soluong * n.DongiaBan).Value.ToString());
            }
            Session["Tong"] = Tong;
            ViewBag.Tong = Tong;/* return Tong view Viewbag tong*/
            return RedirectToAction("Index");
        }

        public decimal ThongKeTongDoanhThu()
        {
            decimal TongDoanhThu = (decimal)db.ChiTietXuatKhoes.Sum(n => n.Soluong * n.DongiaBan);

            return TongDoanhThu;
        }

        public decimal ThongKeTongVon()
        {
            decimal TongVon = (decimal)db.ChiTietNhapKhoes.Sum(n => n.Soluong * n.DongiaNhap);

            return TongVon;
        }
    }
}