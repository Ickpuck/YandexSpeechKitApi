using System.Diagnostics;
using System.Runtime.InteropServices;

namespace YandexSpeechKit.Services
{
    public static class ShellHelper
    {
        public static string Cmd(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            string fileName, arguments;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                fileName = "/bin/bash";
                arguments = $"-c \"{escapedArgs}\"";
            }
            else
            {
                fileName = "cmd.exe";
                arguments = $"/C {escapedArgs}";
            }

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}
