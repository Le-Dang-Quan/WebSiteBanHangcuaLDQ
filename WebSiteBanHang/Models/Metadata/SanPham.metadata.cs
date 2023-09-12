using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    [MetadataTypeAttribute(typeof(SanPhamMetadata))]

    public partial class SanPham
    {
        internal sealed class SanPhamMetadata
        {
           
            public int MaSP { get; set; }
             [DisplayName("Tên SP")]
            public string TenSP { get; set; }
             [DisplayName("Đơn giá")]
            public Nullable<decimal> DonGia { get; set; }
            [DisplayName("Giá cũ")]
            public Nullable<decimal> GiaCu { get; set; }
            [DisplayName("Ngày cập nhật")]
            public Nullable<System.DateTime> NgayCapNhap { get; set; }
             [DisplayName("Cấu hình")]
            public string CauHinh { get; set; }
             [DisplayName("Mô tả")]
            public string MoTa { get; set; }
             [DisplayName("Hình ảnh")]
            public string HinhAnh { get; set; }
             [DisplayName("Số lượng tồn")]
            public Nullable<int> SoLuongTon { get; set; }
             [DisplayName("Lượt xem")]
            public Nullable<int> LuotXem { get; set; }
             [DisplayName("Lượt bình chọn")]
            public Nullable<int> LuotBinhChon { get; set; }
             [DisplayName("Lượt bình luận")]
            public Nullable<int> LuotBinhLuan { get; set; }
             [DisplayName("Số lần mua")]
            public Nullable<int> SoLanMua { get; set; }
             [DisplayName("Mới")]
            public Nullable<int> Moi { get; set; }
             [DisplayName("Nhà cung cấp")]
            public Nullable<int> MaNCC { get; set; }
             [DisplayName("Hãng sản xuất")]
            public Nullable<int> MaNSX { get; set; }
             [DisplayName("Loại SP")]
            public Nullable<int> MaLoaiSP { get; set; }
             [DisplayName("Đã xóa")]
            public Nullable<bool> DaXoa { get; set; }
           
                       [DisplayName("Hình ảnh 1")]
            public string HinhAnh1 { get; set; }
                       [DisplayName("Hình ảnh 2")]
            public string HinhAnh2 { get; set; }
                       [DisplayName("Hình ảnh 3")]
            public string HinhAnh3 { get; set; }
            [DisplayName("Phân Loại")]
            public string PhanLoai { get; set; }

            [DisplayName("Tên Phân Loại")]
            public string TenPhanLoai { get; set; }
            [DisplayName("Sale New")]
            public string SaleNew { get; set; }



        }


    }
}