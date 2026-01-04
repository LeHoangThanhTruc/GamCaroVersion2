using System;
using System.Configuration;
using System.IO;

namespace GameCaro
{
    /// <summary>
    /// Class quản lý cấu hình kết nối Server
    /// Lưu IP Server vào file config để tái sử dụng
    /// </summary>
    public class ServerConfig
    {
        private static ServerConfig instance;
        public static ServerConfig Instance
        {
            get
            {
                if (instance == null)
                    instance = new ServerConfig();
                return instance;
            }
        }

        private readonly string configFilePath;
        private string serverIP;

        private ServerConfig()
        {
            // Lưu config trong thư mục AppData
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, "GameCaro");

            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);

            configFilePath = Path.Combine(appFolder, "server.config");

            // Load IP từ file (nếu có)
            LoadServerIP();
        }

        /// <summary>
        /// Lấy địa chỉ IP Server hiện tại
        /// </summary>
        public string GetServerIP()
        {
            return string.IsNullOrEmpty(serverIP) ? "127.0.0.1" : serverIP;
        }

        /// <summary>
        /// Lưu địa chỉ IP Server mới
        /// </summary>
        public void SaveServerIP(string ip)
        {
            try
            {
                serverIP = ip;
                File.WriteAllText(configFilePath, ip);
                Console.WriteLine($"Đã lưu Server IP: {ip}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lưu Server IP: {ex.Message}");
            }
        }

        /// <summary>
        /// Load địa chỉ IP Server từ file config
        /// </summary>
        private void LoadServerIP()
        {
            try
            {
                if (File.Exists(configFilePath))
                {
                    serverIP = File.ReadAllText(configFilePath).Trim();
                    Console.WriteLine($"Đã load Server IP: {serverIP}");
                }
                else
                {
                    serverIP = "127.0.0.1"; // Default localhost
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi đọc Server IP: {ex.Message}");
                serverIP = "127.0.0.1";
            }
        }

        /// <summary>
        /// Reset về localhost
        /// </summary>
        public void ResetToLocalhost()
        {
            SaveServerIP("127.0.0.1");
        }

        /// <summary>
        /// Kiểm tra định dạng IP có hợp lệ không
        /// </summary>
        public static bool IsValidIP(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return false;

            string[] parts = ip.Split('.');
            if (parts.Length != 4)
                return false;

            foreach (string part in parts)
            {
                if (!byte.TryParse(part, out byte value))
                    return false;
            }

            return true;
        }
    }
}