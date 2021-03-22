using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SwapiNet.Core.Attributes;

namespace SwapiNet.Core.Models
{
    public class Person : BaseModel
    {
        [JsonPropertyName("birth_year")]
        public string BirthYear { get; set; }

        [JsonPropertyName("eye_color")]
        public string EyeColor { get; set; }

        [JsonPropertyName("films")]
        public List<string> FilmEndpoints { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("hair_color")]
        public string HairColor { get; set; }

        [JsonPropertyName("height")]
        public string Height { get; set; }

        [JsonPropertyName("homeworld")]
        public string Homeworld { get; set; }

        [JsonPropertyName("mass")]
        public string Mass { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("skin_color")]
        public string SkinColor { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("edited")]
        public DateTime Edited { get; set; }

        [JsonPropertyName("species")]
        public List<string> SpeciesEndpoints { get; set; }

        [JsonPropertyName("starships")]
        public List<string> StarshipEndpoints { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("vehicles")]
        public List<string> VehicleEndpoints { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(FilmEndpoints))]
        public List<Film> Films { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(SpeciesEndpoints))]
        public List<Species> Species { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(StarshipEndpoints))]
        public List<Starship> Starships { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(VehicleEndpoints))]
        public List<Vehicle> Vehicles { get; set; }
    }
}
