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
        public ActionResult Index(string SearchString = "")
        {
            //if(Session["getMaNK"] != null)
            //{
            //    NhapKho nhapkho = db.NhapKhoes.Find(Session["getMaNK"]);
            //    return View(nhapkho);
            //}
            if (SearchString != "")
            {
                var nhapKhoes = db.NhapKhoes.Where(
                x => x.MaNK.ToUpper().Contains(SearchString.ToUpper())).Include(n => n.Nhanvien);
                return View(nhapKhoes.ToList());
            }
            else
            {
                var nhapKhoes = db.NhapKhoes.Include(n => n.Nhanvien);
                return View(nhapKhoes.ToList());
            }
            
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
            
           
            if (ModelState.IsValid)
            {
                 NhapKho NK = new NhapKho();
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
                
                    NK.MaNK = form["MaNK"];
                    //NK.MaNV = int.Parse(form["NhanVien"]);
                    NK.MaNV = int.Parse(form["NhanVien"]); 
                    NK.NgayLapPhieu = DateTime.Parse(form["NgayLapPhieu"]) ;
                    db.NhapKhoes.Add(NK);
                    db.SaveChanges();
                
                string mank = form["MaNK"];
                List<TonKho> listTK = db.TonKhoes.ToList();
                
                foreach (var item in giohang)
                {
                    ChiTietNhapKho CTNK = new ChiTietNhapKho();
                    TonKho itmeTK = listTK.FirstOrDefault(t => t.MaVT == item.MaVT);
                    //TonKho tonkho = new TonKho();
                    CTNK.MaNK = mank;
                    CTNK.MaVT = item.MaVT;
                    CTNK.Soluong = (short?)item.SoLuongt;
                    CTNK.DongiaNhap = (float?)item.DonGiaNhapt;
                    //this.Insert(CTNK);
                    //themTonKho
                    db.ChiTietNhapKhoes.Add(CTNK);
                    db.SaveChanges();
                    if (itmeTK != null )
                    {
                        itmeTK.MaVT = itmeTK.MaVT;
                        itmeTK.NgayLap = itmeTK.NgayLap;
                        itmeTK.GiaNhap = (float?)item.DonGiaNhapt;
                        itmeTK.GiaXuat = itmeTK.GiaXuat;
                        itmeTK.SLNhap +=  item.SoLuongt;
                        int sl = (int)itmeTK.SLNhap;
                        itmeTK.SLCuoi = sl - itmeTK.SLXuat;
                        //itmeTK.SLCuoi = sl - 1;
                        db.Entry(itmeTK).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        TonKho tonkhoIN = new TonKho();
                        tonkhoIN.MaVT = item.MaVT;
                        tonkhoIN.NgayLap = form["NgayLapPhieu"];
                        tonkhoIN.GiaNhap = (float?)item.DonGiaNhapt;
                        tonkhoIN.SLCuoi = item.SoLuongt;
                        tonkhoIN.SLNhap = item.SoLuongt;
                        db.TonKhoes.Add(tonkhoIN);
                        db.SaveChanges();
                    }
                    
                }
                Session["giohang"] = null;
                //Session["getMaNK"] = form["MaNK"];
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}