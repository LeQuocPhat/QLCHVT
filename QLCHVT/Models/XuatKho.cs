//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLCHVT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class XuatKho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XuatKho()
        {
            this.ChiTietXuatKhoes = new HashSet<ChiTietXuatKho>();
        }
    
        public string MaXK { get; set; }
        public string MaKH { get; set; }
        public Nullable<int> MaNV { get; set; }
        public System.DateTime NgayLapPhieu { get; set; }
        public System.DateTime NgayGiaoHang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietXuatKho> ChiTietXuatKhoes { get; set; }
        public virtual Nhanvien Nhanvien { get; set; }
    }
}
