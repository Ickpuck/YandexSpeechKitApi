using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YandexSpeechKit.Models
{
    public class SpeechRequestModel
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("ssml")]
        public string Ssml { get; set; }

        [JsonProperty("voice")]
        public string Voice { get; set; }

        [JsonProperty("emotion")]
        public string Emotion { get; set; }

        [JsonProperty("speed")]
        public string Speed { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("sampleRateHertz")]
        public string SampleRateHertz { get; set; }

        [JsonProperty("folderId")]
        public string FolderId { get; set; }

        public SpeechRequestModel()
        {
            Lang = "ru-RU";
            Format = "lpcm";
            SampleRateHertz = "48000";
        }

        public Dictionary<string, string> ToDict()
        {
            var dict = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(Text)) dict.Add("text", Text);
            if (!String.IsNullOrEmpty(Ssml)) dict.Add("ssml", Ssml);
            if (!String.IsNullOrEmpty(Lang)) dict.Add("lang", Lang);
            if (!String.IsNullOrEmpty(Voice)) dict.Add("voice", Voice);
            if (!String.IsNullOrEmpty(Speed)) dict.Add("speed", Speed);
            if (!String.IsNullOrEmpty(Format)) dict.Add("format", Format);
            if (!String.IsNullOrEmpty(SampleRateHertz)) dict.Add("sampleRateHertz", SampleRateHertz);
            if (!String.IsNullOrEmpty(FolderId)) dict.Add("folderId", FolderId);

            return dict;
        }

        public Dictionary<string, string> Validate()
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            string[] possibleRate = new string[] { "48000", "16000", "8000" };

            if (String.IsNullOrEmpty(this.Text) & String.IsNullOrEmpty(this.Ssml))
                errors.Add("TextForSpeech", "Должен быть указан ssml или text");

            if (!String.IsNullOrEmpty(this.Text) & !String.IsNullOrEmpty(this.Ssml))
                errors.Add("TextOrSsml", "Укажите только ssml или text");

            if (String.IsNullOrEmpty(this.SampleRateHertz) || !possibleRate.Contains(this.SampleRateHertz))
                errors.Add("SampleRateHertz", "Возможные значения для sampleRateHertz: 48000, 16000, 8000");

            return errors;
        }
    }
}
