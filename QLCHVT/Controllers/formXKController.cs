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
    public class formXKController : Controller 
    { 
    private QLCHVTEntities db = new QLCHVTEntities();
    // GET: formNK
    public ActionResult Index()
    {
        var xuatKho = db.XuatKhoes.Include(n => n.Nhanvien);
        return View(xuatKho.ToList());
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
            newCart.DonGiaBan = Convert.ToDouble(sp.Dongia);
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

        XuatKho NK = new XuatKho();
        List<CartItem> giohang = Session["giohang"] as List<CartItem>;
        if (ModelState.IsValid)
        {

            NK.MaXK = form["MaXK"];
            //NK.MaNV = int.Parse(form["NhanVien"]);
            NK.MaNV = int.Parse(form["NhanVien"]);
            NK.NgayLapPhieu = DateTime.Parse(form["NgayLapPhieu"]);
            NK.NgayGiaoHang = DateTime.Parse(form["NgayGiaoHang"]);
                db.XuatKhoes.Add(NK);
            db.SaveChanges();

            string maxk = form["MaXK"];
            List<TonKho> listTK = db.TonKhoes.ToList();

            foreach (var item in giohang)
            {
                ChiTietXuatKho CTNK = new ChiTietXuatKho();
                TonKho itmeTK = listTK.FirstOrDefault(t => t.MaVT == item.MaVT);
                //TonKho tonkho = new TonKho();
                CTNK.MaXK = maxk;
                CTNK.MaVT = item.MaVT;
                CTNK.Soluong = (short?)item.SoLuongt;
                CTNK.DongiaBan = (float?)item.DonGiaBan;
                //this.Insert(CTNK);
                //themTonKho
                db.ChiTietXuatKhoes.Add(CTNK);
                db.SaveChanges();
                if (itmeTK != null)
                {
                        itmeTK.MaVT = itmeTK.MaVT;
                    itmeTK.NgayLap = itmeTK.NgayLap;
                    itmeTK.GiaXuat = float.Parse(form["GiaBan"]);
                    itmeTK.SLXuat = itmeTK.SLXuat + item.SoLuongt;
                    int sl = (int)itmeTK.SLNhap;
                    int sx = (int)itmeTK.SLXuat;
                    
                    itmeTK.SLCuoi = sl - sx;
                        if (itmeTK.SLCuoi == 0)
                        {
                            itmeTK.SLXuat = 0;
                            itmeTK.SLNhap = 0;
                            itmeTK.SLCuoi = 0;
                            db.Entry(itmeTK).State = EntityState.Modified;
                            db.SaveChanges();
                            Session["giohang"] = null;
                            return RedirectToAction("Index");
                        }
                        //itmeTK.SLCuoi = sl - 1;
                        db.Entry(itmeTK).State = EntityState.Modified;
                    db.SaveChanges();
                     Session["giohang"] = null;
                }
                else
                {
                        return RedirectToAction("Index");
                 }

            }
               
            return RedirectToAction("Index");
        }
        return View();
    }
}
}