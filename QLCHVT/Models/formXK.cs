using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLCHVT.Models
{
    public class formXK
    {
        
        public int MaNV { get; set; }
        public string MaVT { get; set; }
        [Required(ErrorMessage = "Please enter MaXK name.")]
        [Display(Name = "Mã xuất kho")]
        public string MaXK { get; set; }
        public double DonGiaNhap { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayLapPhieu { get; set; }
        public DateTime NgayGiaoHang { get; set; }
    }
}