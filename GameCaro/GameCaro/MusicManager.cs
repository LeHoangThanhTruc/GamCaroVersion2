using System.IO;
using WMPLib;
using System.Windows.Forms;


namespace GameCaro
{
    public static class MusicManager
    {
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static bool isPlaying = false;

        private static string musicPath =
            Path.Combine(Application.StartupPath, "music.mp3");

        public static void Play()
        {
            if (!AppSettings.IsMusicEnabled) return;
            if (isPlaying) return;

            player.URL = musicPath;
            player.settings.setMode("loop", true);
            player.controls.play();
            isPlaying = true;
        }

        public static void Stop()
        {
            player.controls.stop();
            isPlaying = false;
        }

        public static void UpdateState()
        {
            if (AppSettings.IsMusicEnabled)
                Play();
            else
                Stop();
        }
    }
}
