using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLCHVT.Models
{
    public class ViewModel
    {

        public KhachHang Khachhang { get; set; }
        public VatTu vattu { get; set; }
        public ChiTietNhapKho ctnk { get; set; }
        public  LoaiVatTu lvt { get; set; }
        public Nhanvien nhanvien { get; set; }
        public NhapKho nhapkho { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public double Thanhtien { get; set; }
    }
}