using System;
using Microsoft.AspNetCore.Mvc;
using YandexSpeechKit.Models;
using YandexSpeechKit.Services;

namespace YandexSpeechKit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechController : ControllerBase
    {
        private SpeechHttpClient _client;
        private SpeechFileWriter _fileWriter;

        public SpeechController(SpeechHttpClient client, SpeechFileWriter fileWriter)
        {
            _client = client;
            _fileWriter = fileWriter;
        }

        [HttpPost]
        public ActionResult Post(SpeechRequestModel request)
        {
            var errors = request.Validate();
            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return BadRequest(ModelState);
            }
            
            var wavData = _client.get(request);
            var directory = _fileWriter.WriteToWav(wavData.Result, Convert.ToInt32(request.SampleRateHertz));
            return Ok(directory);
        }

    }
}