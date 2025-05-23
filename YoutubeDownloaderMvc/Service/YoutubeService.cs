using System.Diagnostics;

namespace YoutubeDownloaderMvc.Service;

public class YoutubeService
{
    public async Task<string> DownloadAudioAsync(string videoUrl, string outputFolder)
    {
        Directory.CreateDirectory(outputFolder);
        string fileName = $"audio_{DateTime.Now.Ticks}.mp3";

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "yt-dlp",
                Arguments = $"-x --audio-format mp3 --output \"{fileName}\" {videoUrl}",
                WorkingDirectory = outputFolder,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        await process.WaitForExitAsync();

        return fileName;
    }
}