using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WillowTree.NameGame.Core.Models;

namespace WillowTree.NameGame.Core.Services
{
    public class NameGameService
    {
        private const string DataUrl = "https://www.willowtreeapps.com/api/v1.0/profiles";

        public async Task<List<Profile>> GetProfiles()
        {
            List<Profile> Profiles = new List<Profile>();
            try
            {
                string JSONResult = string.Empty;
                using (HttpClient Client = new HttpClient())
                {
                    Client.Timeout = new TimeSpan(50000000); // 5 seconds
                    JSONResult = await Client.GetStringAsync(DataUrl);
                }

                if (!string.IsNullOrEmpty(JSONResult))
                {
                    Newtonsoft.Json.Linq.JArray profiles = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(JSONResult);

                    foreach (Newtonsoft.Json.Linq.JToken profile in profiles)
                        Profiles.Add(new Profile(profile));
                }
                

                return Profiles;
            }
            catch (HttpRequestException)
            {
                // Failed to send request to data url
                throw;
            }
            catch (InvalidCastException)
            {
                // Failed to parse JSON
                throw;
            }
            catch (Exception)
            {
                // Shouldn't hit here but any other exceptions..
                throw;
            }
        }

        public List<Profile> PickProfiles(List<Profile> Profiles, int NumProfiles)
        {
            List<Profile> PickedProfiles = new List<Profile>(NumProfiles);
            List<int> PickedProfileIndices = new List<int>(NumProfiles);
            int index = -1;
            Random random = new Random();

            for (int i = 0; i < NumProfiles; i++)
            {
                index = random.Next(0, Profiles.Count - 1);
                while (PickedProfileIndices.Contains(index))
                    index = random.Next(0, Profiles.Count - 1);
                PickedProfileIndices.Add(index);
                PickedProfiles.Add(Profiles[index]);
            }

            return PickedProfiles;
        }
    }
}
