using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLCHVT.Models
{
    public class ViewModelXK
    {

        public KhachHang Khachhang { get; set; }
        public VatTu vattu { get; set; }
        public ChiTietXuatKho ctxk { get; set; }
        public LoaiVatTu lvt { get; set; }
        public Nhanvien nhanvien { get; set; }
        public XuatKho xuatkho { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public double Thanhtien { get; set; }
    }
}