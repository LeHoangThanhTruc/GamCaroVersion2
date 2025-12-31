using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace GameCaro
{
    public class SessionManager
    {
        private static SessionManager instance;
        public static SessionManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SessionManager();
                return instance;
            }
        }

        private readonly string sessionFilePath;
        private SessionData currentSession;

        // Key và IV cho AES encryption (trong thực tế nên lưu ở nơi an toàn hơn)
        private readonly byte[] encryptionKey;
        private readonly byte[] encryptionIV;

        private SessionManager()
        {
            // Lưu file session trong thư mục AppData
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, "GameCaro");

            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);

            sessionFilePath = Path.Combine(appFolder, "session.dat");

            // Tạo key và IV từ machine-specific data (mỗi máy khác nhau)
            string machineKey = Environment.MachineName + Environment.UserName;
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(machineKey));
                encryptionKey = new byte[32]; // AES-256
                encryptionIV = new byte[16];
                Array.Copy(hash, 0, encryptionKey, 0, 32);
                Array.Copy(hash, 16, encryptionIV, 0, 16);
            }
        }

        // Lưu session khi đăng nhập thành công
        public void SaveSession(string userId, string username, string sessionToken, bool rememberMe = true)
        {
            try
            {
                currentSession = new SessionData
                {
                    UserId = userId,
                    Username = username,
                    LoginTime = DateTime.Now,
                    RememberMe = rememberMe,
                    SessionToken = sessionToken // Token từ server
                };

                string json = JsonConvert.SerializeObject(currentSession);

                // Mã hóa AES để bảo mật
                string encrypted = EncryptStringAES(json);
                File.WriteAllText(sessionFilePath, encrypted);

                Console.WriteLine($"Đã lưu session cho {username}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lưu session: " + ex.Message);
            }
        }

        // Đọc session khi khởi động app
        public SessionData LoadSession()
        {
            try
            {
                if (!File.Exists(sessionFilePath))
                    return null;

                string encrypted = File.ReadAllText(sessionFilePath);
                string json = DecryptStringAES(encrypted);

                currentSession = JsonConvert.DeserializeObject<SessionData>(json);

                // Kiểm tra session còn hiệu lực không (7 ngày)
                if (currentSession != null && currentSession.RememberMe)
                {
                    TimeSpan duration = DateTime.Now - currentSession.LoginTime;
                    if (duration.TotalDays > 7)
                    {
                        // Session hết hạn
                        ClearSession();
                        return null;
                    }

                    Console.WriteLine($"Tìm thấy session: {currentSession.Username}");
                    return currentSession;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi đọc session: " + ex.Message);
                ClearSession();
                return null;
            }
        }

        // Xóa session khi đăng xuất
        public void ClearSession()
        {
            try
            {
                if (File.Exists(sessionFilePath))
                    File.Delete(sessionFilePath);

                currentSession = null;
                Console.WriteLine("Đã xóa session");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xóa session: " + ex.Message);
            }
        }

        // Lấy session hiện tại
        public SessionData GetCurrentSession()
        {
            return currentSession;
        }

        // Kiểm tra đã đăng nhập chưa
        public bool IsLoggedIn()
        {
            return currentSession != null;
        }

        // Mã hóa AES-256
        private string EncryptStringAES(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.IV = encryptionIV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        // Giải mã AES-256
        private string DecryptStringAES(string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.IV = encryptionIV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream msDecrypt = new MemoryStream(buffer))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

    // Class chứa thông tin session
    public class SessionData
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public DateTime LoginTime { get; set; }
        public bool RememberMe { get; set; }
        public string SessionToken { get; set; }
    }
}