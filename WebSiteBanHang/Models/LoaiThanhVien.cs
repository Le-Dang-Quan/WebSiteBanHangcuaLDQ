//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoaiThanhVien
    {
        public LoaiThanhVien()
        {
            this.ThanhViens = new HashSet<ThanhVien>();
        }
    
        public int MaLoaiTV { get; set; }
        public string TenLoai { get; set; }
        public Nullable<int> UuDai { get; set; }
    
        public virtual ICollection<ThanhVien> ThanhViens { get; set; }
    }
}
