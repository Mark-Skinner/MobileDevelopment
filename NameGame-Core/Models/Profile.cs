using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WillowTree.NameGame.Core.Models
{
	public class Profile
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("slug")]
		public string Slug { get; set; }

		[JsonProperty("jobTitle")]
		public string JobTitle { get; set; }

		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }

		[JsonProperty("headshot")]
		public Headshot Headshot { get; set; }

		public string FullName
		{
			get { return $"{FirstName} {LastName}"; }
		}

        public Profile(Newtonsoft.Json.Linq.JToken Profile)
        {
            Id = Profile.Value<string>("id");
            Type = Profile.Value<string>("type");
            Slug = Profile.Value<string>("slug");
            JobTitle = Profile.Value<string>("jobTitle");
            FirstName = Profile.Value<string>("firstName");
            LastName = Profile.Value<string>("lastName");
            Headshot = new Headshot(Profile.Value<Newtonsoft.Json.Linq.JToken>("headshot"));
        }
    }
}
