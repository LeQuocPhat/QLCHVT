using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHVT.Models
{
    public class CartItem
    {
        public string MaVT { get; set; }
        public string TenVT { get; set; }
        public int SoLuongt { get; set; }
        public double DonGiaNhapt { get; set; }
        public double DonGiaBan { get; set; }
        public double ThanhTien
        {
            get
            {
                return DonGiaNhapt * SoLuongt;
            }
        }

    }
}