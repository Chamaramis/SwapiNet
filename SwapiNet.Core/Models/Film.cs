using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SwapiNet.Core.Attributes;

namespace SwapiNet.Core.Models
{
    public class Film : BaseModel
    {
        [JsonPropertyName("characters")]
        public List<string> CharacterEndpoints { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("director")]
        public string Director { get; set; }

        [JsonPropertyName("edited")]
        public DateTime Edited { get; set; }

        [JsonPropertyName("episode_id")]
        public int EpisodeId { get; set; }

        [JsonPropertyName("opening_crawl")]
        public string OpeningCrawl { get; set; }

        [JsonPropertyName("planets")]
        public List<string> PlanetEndpoints { get; set; }

        [JsonPropertyName("producer")]
        public string Producer { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("species")]
        public List<string> SpeciesEndpoints { get; set; }

        [JsonPropertyName("starships")]
        public List<string> StarshipEndpoints { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("vehicles")]
        public List<string> VehicleEndpoints { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(CharacterEndpoints))]
        public List<Person> Characters { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(PlanetEndpoints))]
        public List<Planet> Planets { get; set; }

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
