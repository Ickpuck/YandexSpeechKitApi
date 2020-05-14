using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace YandexSpeechKit.Services
{
    public class SpeechFileWriter
    {
        private IConfiguration _config;
        public SpeechFileWriter(IConfiguration config)
        {
            _config = config;
        }

        public string WriteToWav(byte[] input, int rate)
        {
            string pathWave = this._config.GetValue<string>("WavOutputDirectory") + generateFileName(".wav");

            MemoryStream newFile = new MemoryStream(48 + input.Length);
            newFile.Write(Encoding.UTF8.GetBytes("RIFF"), 0, 4);
            newFile.Write(BitConverter.GetBytes(input.Length + 40), 0, 4);
            newFile.Write(Encoding.UTF8.GetBytes("WAVE"), 0, 4); 
            newFile.Write(Encoding.UTF8.GetBytes("fmt "), 0, 4);
            newFile.Write(BitConverter.GetBytes(18), 0, 4);
            newFile.Write(BitConverter.GetBytes(1), 0, 2);
            newFile.Write(BitConverter.GetBytes(1), 0, 2);
            newFile.Write(BitConverter.GetBytes(rate), 0, 4);
            newFile.Write(BitConverter.GetBytes(rate), 0, 4);
            newFile.Write(BitConverter.GetBytes(1), 0, 2);
            newFile.Write(BitConverter.GetBytes(16), 0, 4);
            newFile.Write(Encoding.UTF8.GetBytes("data"), 0, 4);
            newFile.Write(BitConverter.GetBytes(input.Length), 0, 4);

            newFile.Write(input, 0, input.Length);
            newFile.Close();
            File.WriteAllBytes(pathWave, newFile.GetBuffer());
            return pathWave;
        }

        private string generateFileName(string extension)
        {
            return $@"{Guid.NewGuid()}" + extension;
        }
    }
}
