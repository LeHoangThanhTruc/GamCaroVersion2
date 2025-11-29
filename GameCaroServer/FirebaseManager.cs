using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCaroShared;
namespace GameCaroServer
{
    public class FirebaseManager
    {
        // Bạn sẽ dùng Firebase Admin SDK hoặc Realtime Database SDK
        // Skeleton ví dụ

        public FirebaseManager()
        {
            // Khởi tạo Firebase ở đây
        }

        public async Task SaveMoveAsync(PlayInfor move)
        {
            // Ghi move vào Firebase
            await Task.CompletedTask;
        }

        public async Task<GameState> LoadGameStateAsync()
        {
            // Load trạng thái game từ Firebase
            return await Task.FromResult(new GameState());
        }

        public async Task ResetGameAsync()
        {
            // Xóa trạng thái game trên Firebase
            await Task.CompletedTask;
        }
    }
}
