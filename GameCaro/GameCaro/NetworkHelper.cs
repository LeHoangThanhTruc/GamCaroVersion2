using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GameCaro
{
    /// <summary>
    /// Class hỗ trợ các chức năng liên quan đến mạng
    /// </summary>
    public static class NetworkHelper
    {
        /// <summary>
        /// Lấy địa chỉ IPv4 của máy hiện tại (tự động chọn interface phù hợp)
        /// </summary>
        /// <returns>Địa chỉ IPv4 hoặc "127.0.0.1" nếu không tìm thấy</returns>
        public static string GetLocalIPv4()
        {
            try
            {
                // Ưu tiên: Wi-Fi > Ethernet > Các adapter khác
                var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.OperationalStatus == OperationalStatus.Up)
                    .OrderByDescending(ni => GetInterfacePriority(ni.NetworkInterfaceType));

                foreach (var ni in interfaces)
                {
                    var properties = ni.GetIPProperties();
                    var ipv4 = properties.UnicastAddresses
                        .Where(ua => ua.Address.AddressFamily == AddressFamily.InterNetwork)
                        .Where(ua => !IPAddress.IsLoopback(ua.Address))
                        .Select(ua => ua.Address)
                        .FirstOrDefault();

                    if (ipv4 != null)
                    {
                        return ipv4.ToString();
                    }
                }

                return "127.0.0.1";
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// Lấy tất cả địa chỉ IPv4 của máy
        /// </summary>
        public static List<NetworkInterfaceInfo> GetAllLocalIPv4()
        {
            var result = new List<NetworkInterfaceInfo>();

            try
            {
                var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.OperationalStatus == OperationalStatus.Up);

                foreach (var ni in interfaces)
                {
                    var properties = ni.GetIPProperties();
                    var ipAddresses = properties.UnicastAddresses
                        .Where(ua => ua.Address.AddressFamily == AddressFamily.InterNetwork)
                        .Where(ua => !IPAddress.IsLoopback(ua.Address))
                        .Select(ua => ua.Address.ToString())
                        .ToList();

                    if (ipAddresses.Any())
                    {
                        result.Add(new NetworkInterfaceInfo
                        {
                            Name = ni.Name,
                            Description = ni.Description,
                            Type = ni.NetworkInterfaceType.ToString(),
                            IPAddresses = ipAddresses
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting network interfaces: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Lấy IP Public của máy (qua dịch vụ web)
        /// </summary>
        public static string GetPublicIP()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "Mozilla/5.0");
                    string ip = client.DownloadString("https://api.ipify.org").Trim();
                    return ip;
                }
            }
            catch
            {
                return "Không xác định";
            }
        }

        /// <summary>
        /// Kiểm tra xem IP có phải là IP Private (LAN) không
        /// </summary>
        public static bool IsPrivateIP(string ipAddress)
        {
            if (!IPAddress.TryParse(ipAddress, out IPAddress ip))
                return false;

            byte[] bytes = ip.GetAddressBytes();

            // Class A: 10.0.0.0 – 10.255.255.255
            if (bytes[0] == 10)
                return true;

            // Class B: 172.16.0.0 – 172.31.255.255
            if (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31)
                return true;

            // Class C: 192.168.0.0 – 192.168.255.255
            if (bytes[0] == 192 && bytes[1] == 168)
                return true;

            return false;
        }

        /// <summary>
        /// Kiểm tra kết nối đến một Server
        /// </summary>
        public static bool TestConnection(string ipAddress, int port, int timeoutMs = 3000)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(ipAddress, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeoutMs);

                    if (!success)
                    {
                        return false;
                    }

                    client.EndConnect(result);
                    return client.Connected;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Định mức ưu tiên cho các loại interface
        /// </summary>
        private static int GetInterfacePriority(NetworkInterfaceType type)
        {
            switch (type)
            {
                case NetworkInterfaceType.Wireless80211:
                    return 10; // Wi-Fi - ưu tiên cao nhất
                case NetworkInterfaceType.Ethernet:
                    return 9;  // Ethernet - ưu tiên thứ 2
                case NetworkInterfaceType.GigabitEthernet:
                    return 8;
                case NetworkInterfaceType.FastEthernetT:
                case NetworkInterfaceType.FastEthernetFx:
                    return 7;
                default:
                    return 1;
            }
        }

        /// <summary>
        /// Tạo message box hiển thị thông tin mạng
        /// </summary>
        public static string GetNetworkInfoText()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("📡 THÔNG TIN MẠNG\n");

            var interfaces = GetAllLocalIPv4();

            if (interfaces.Any())
            {
                sb.AppendLine("🔌 Các kết nối:");
                foreach (var ni in interfaces)
                {
                    sb.AppendLine($"\n• {ni.Name} ({ni.Type})");
                    foreach (var ip in ni.IPAddresses)
                    {
                        sb.AppendLine($"  IP: {ip}");
                    }
                }
            }
            else
            {
                sb.AppendLine("❌ Không tìm thấy kết nối mạng nào!");
            }

            sb.AppendLine($"\n🌐 IP Public: {GetPublicIP()}");
            sb.AppendLine($"\n💡 Khuyến nghị dùng IP: {GetLocalIPv4()}");

            return sb.ToString();
        }
    }

    /// <summary>
    /// Thông tin về một network interface
    /// </summary>
    public class NetworkInterfaceInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public List<string> IPAddresses { get; set; }

        public override string ToString()
        {
            return $"{Name} - {string.Join(", ", IPAddresses)}";
        }
    }
}