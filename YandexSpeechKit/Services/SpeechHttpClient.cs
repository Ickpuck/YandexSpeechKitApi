using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using YandexSpeechKit.Models;

namespace YandexSpeechKit.Services
{

    public class SpeechHttpClient
    {
        private string _baseUrl;
        private string _folderId;
        private HttpClient _client;
        private IConfiguration _config;

        public SpeechHttpClient(IConfiguration config)
        {
            _client = new HttpClient();
            _config = config;
            _baseUrl = _config.GetValue<string>("YandexSpeechGeneratorUrl");
            _folderId = _config.GetValue<string>("YandexFolderId");
        }

        public async Task<byte[]> get(SpeechRequestModel request)
        {
            string token = ShellHelper.Cmd("yc iam create-token").Trim();
            request.FolderId = _folderId;
            var content = new FormUrlEncodedContent(request.ToDict());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync(_baseUrl, content);
            var result = response.Content.ReadAsByteArrayAsync().Result;
            return result;
        }
    }
}
