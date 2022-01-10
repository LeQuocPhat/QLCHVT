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
            if (Session["giohang"] == null)//chuw alive tao de add vaof carrt
            {
                Session["giohang"] = new List<CartItem>();
            }
            
            List<Nhanvien> NhanVien = db.Nhanviens.ToList();
            //// Tạo SelectList
            SelectList nhanVien = new SelectList(NhanVien, "MaNV", "Ten");
            //// Set vào ViewBag
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.NhanVien = nhanVien;
            ViewBag.GioHang = giohang;
            return View();
        }

        public RedirectToRouteResult AddToForm(string MaSP)
        {
           
            //update carrt
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            //kiemtra gio hang co sp dax chonj ch, neu chua co add
            if (giohang.FirstOrDefault(c => c.MaVT == MaSP) == null)//return câu select đầu tiên
            {
                VatTu sp = db.VatTus.Find(MaSP);
                CartItem newCart = new CartItem();
                newCart.MaVT = MaSP;
                newCart.TenVT = sp.TenVT;
                newCart.SoLuongt = 1;
                newCart.DonGiaNhapt = Convert.ToDouble(sp.Dongia);
                giohang.Add(newCart);
            }
            else //sp đã có trong cart và tăng sp
            {
                CartItem item = giohang.FirstOrDefault(c => c.MaVT == MaSP);//tìm phần tử cartItem trong gio hàng theo MaSp
                item.SoLuongt++;
            }
            Session["giohang"] = giohang;
            ViewBag.GioHang = giohang;
            return RedirectToAction("AddOrder");

        }

        public ActionResult SelectST()
        {

            var vatTus = db.VatTus.Include(v => v.LoaiVatTu);
            return View(vatTus.ToList());
        }

        public RedirectToRouteResult Delete(string MaSP)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem item = giohang.FirstOrDefault(c => c.MaVT == MaSP);
            if (item != null)
            {
                giohang.Remove(item);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("AddOrder");
        }

        //public int Insert(ChiTietNhapKho ctnk)
        //{
        //    db.ChiTietNhapKhoes.Add(ctnk);
        //    return db.SaveChanges();
        //}

        [HttpPost]
        public ActionResult AddOrder(FormCollection form)
        {
            
            NhapKho NK = new NhapKho();
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            if (ModelState.IsValid)
            {
                
                    NK.MaNK = form["MaNK"];
                    //NK.MaNV = int.Parse(form["NhanVien"]);
                    NK.MaNV = int.Parse(form["NhanVien"]); 
                    NK.NgayLapPhieu = DateTime.Parse(form["NgayLapPhieu"]) ;
                    db.NhapKhoes.Add(NK);
                    db.SaveChanges();
                
                string mank = form["MaNK"];
                
                foreach (var item in giohang)
                {
                    ChiTietNhapKho CTNK = new ChiTietNhapKho();
                    CTNK.MaNK = mank;
                    CTNK.MaVT = item.MaVT;
                    CTNK.Soluong = (short?)item.SoLuongt;
                    CTNK.DongiaNhap = (float?)item.DonGiaNhapt;
                    //this.Insert(CTNK);
                    db.ChiTietNhapKhoes.Add(CTNK);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}