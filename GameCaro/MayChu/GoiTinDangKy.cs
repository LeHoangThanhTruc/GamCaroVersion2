using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayChu
{
    public class GoiTinDangKy
    {
        public string IDUser { get; set; }
        public string HoVaTen { get; set; }
        public string TenTaiKhoan { get; set; }
        public string Gmail { get; set; }
        public string MatKhau { get; set; }
        public bool isVerified { get; set; } = false;
        public string Avatar { get; set; }
        public GoiTinDangKy(string id, string tk, string mk, string email, string hoTen)
        {
            IDUser = id;
            TenTaiKhoan = tk;
            MatKhau = mk;
            Gmail = email;
            HoVaTen = hoTen;
            Avatar = "default.jpg";

        }
    }
}
