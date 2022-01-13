using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHVT.Models
{
    public class formXK
    {
        public int MaNV { get; set; }
        public string MaVT { get; set; }
        public string MaXK { get; set; }
        public double DonGiaNhap { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayLapPhieu { get; set; }
        public DateTime NgayGiaoHang { get; set; }
    }
}