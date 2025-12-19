using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCrypt.Net;
using System.Threading.Tasks;

namespace MayChu
{
    /// <summary>
    /// Class xử lý mã hóa và xác thực mật khẩu sử dụng BCrypt
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Mã hóa mật khẩu plain text thành hash
        /// </summary>
        /// <param name="password">Mật khẩu gốc cần mã hóa</param>
        /// <returns>Chuỗi mật khẩu đã được mã hóa</returns>
        public static string HashPassword(string password)
        {
            // BCrypt tự động tạo salt và hash với work factor mặc định là 11
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Xác thực mật khẩu nhập vào có khớp với hash không
        /// </summary>
        /// <param name="password">Mật khẩu plain text cần kiểm tra</param>
        /// <param name="hashedPassword">Mật khẩu đã hash từ database</param>
        /// <returns>True nếu khớp, False nếu không khớp</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch
            {
                // Nếu hashedPassword không đúng định dạng BCrypt
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra xem chuỗi có phải là BCrypt hash không
        /// </summary>
        /// <param name="password">Chuỗi cần kiểm tra</param>
        /// <returns>True nếu là BCrypt hash</returns>
        public static bool IsHashedPassword(string password)
        {
            // BCrypt hash luôn bắt đầu với $2a$, $2b$, $2x$, hoặc $2y$
            return password != null && password.StartsWith("$2");
        }
    }
}
