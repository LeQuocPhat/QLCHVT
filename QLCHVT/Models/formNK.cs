using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLCHVT.Models
{
    public class formNK
    {
        [Required(ErrorMessage = "Please enter student name.")]
        public int MaNV { get; set; }
        [Required(ErrorMessage = "Please enter student name.")]
        public string MaVT { get; set; }
        [Required(ErrorMessage = "Please enter student name.")]
        public string MaNK { get; set; }
        [Required(ErrorMessage = "Please enter student name.")]
        public double DonGiaNhap { get; set; }
        [Required(ErrorMessage = "Please enter student name.")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Please enter student name.")]
        public DateTime NgayLapPhieu { get; set; }
        [Required(ErrorMessage = "Please enter student name.")]
        public string MaXK { get; set; }
       
    }
}