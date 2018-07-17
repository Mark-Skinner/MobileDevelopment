using System;
using Newtonsoft.Json;

namespace WillowTree.NameGame.Core.Models
{
	public class Headshot
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("mimeType")]
		public string MimeType { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("alt")]
		public string AltText { get; set; }

		[JsonProperty("width")]
		public int Width { get; set; }

		[JsonProperty("height")]
		public int Height { get; set; }

        public Headshot(Newtonsoft.Json.Linq.JToken Headshot)
        {
            Type = Headshot.Value<string>("type");
            MimeType = Headshot.Value<string>("mimeType");
            Id = Headshot.Value<string>("id");
            Url = Headshot.Value<string>("url");
            if (!string.IsNullOrEmpty(Url))
                Url = "http:" + Url;
            AltText = Headshot.Value<string>("alt");
            Width = Headshot.Value<int>("width");
            Height = Headshot.Value<int>("height");
        }
	}
}
