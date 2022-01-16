using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLCHVT.Models
{
    public class formNK
    {   [Display(Name ="Tên nhân viên")]
        [Required(ErrorMessage = "Please enter MaNV name.")]
        public int MaNV { get; set; }
        [Required(ErrorMessage = "Please enter MaVT name.")]
        [Display(Name = "Mã vật tư")]
        public string MaVT { get; set; }
        [Required(ErrorMessage = "Please enter MaNK name.")]
        [Display(Name = "Mã nhập kho")]
        public string MaNK { get; set; }
        [Required(ErrorMessage = "Please enter DonGiaNhap name.")]
        [Display(Name = "Đơn giá nhập")]
        public double DonGiaNhap { get; set; }
        [Required(ErrorMessage = "Please enter SoLuong name.")]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Please enter NgayLapPhieu name.")]
        [Display(Name = "Ngày lập")]
        public DateTime NgayLapPhieu { get; set; }
        [Required(ErrorMessage = "Please enter MaXK name.")]
        [Display(Name = "Mã xuất kho")]
        public string MaXK { get; set; }
       
    }
}