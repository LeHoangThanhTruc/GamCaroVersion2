using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameCaro
{
    public class UserProfile
    {
        public string Avatar { get; set; }
        public string TenTaiKhoan { get; set; }
        public string IDUser { get; set; }
        public string Gmail { get; set; }
        public bool isVerified { get; set; }

        // ⚠️ CÓ THỂ KHÔNG TỒN TẠI TRONG FIREBASE
        [JsonProperty("SoTranDaThamGia")]
        public int? SoTranDaThamGia { get; set; }

        [JsonProperty("SoTranChienThang")]
        public int? SoTranDaChienThang { get; set; }

        [JsonProperty("SoTranThua")]
        public int? SoTranDaThua { get; set; }
    }



}
