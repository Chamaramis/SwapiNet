using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SwapiNet.Core.Attributes;

namespace SwapiNet.Core.Models
{
    public class Species : BaseModel
    {
        [JsonPropertyName("average_height")]
        public string AverageHeight { get; set; }

        [JsonPropertyName("average_lifespan")]
        public string AverageLifespan { get; set; }

        [JsonPropertyName("classification")]
        public string Classification { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("designation")]
        public string Designation { get; set; }

        [JsonPropertyName("edited")]
        public DateTime Edited { get; set; }

        [JsonPropertyName("eye_colors")]
        public string EyeColors { get; set; }

        [JsonPropertyName("hair_colors")]
        public string HairColors { get; set; }

        [JsonPropertyName("homeworld")]
        public string Homeworld { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("people")]
        public List<string> PeopleEndpoints { get; set; }

        [JsonPropertyName("films")]
        public List<string> FilmEndpoints { get; set; }

        [JsonPropertyName("skin_colors")]
        public string SkinColors { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(FilmEndpoints))]
        public List<Film> Films { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(PeopleEndpoints))]
        public List<Person> People { get; set; }
    }
}
