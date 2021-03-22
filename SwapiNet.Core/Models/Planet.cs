using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SwapiNet.Core.Attributes;

namespace SwapiNet.Core.Models
{
    public class Planet : BaseModel
    {
        [JsonPropertyName("climate")]
        public string Climate { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("diameter")]
        public string Diameter { get; set; }

        [JsonPropertyName("edited")]
        public DateTime Edited { get; set; }

        [JsonPropertyName("films")]
        public List<string> FilmEndpoints { get; set; }

        [JsonPropertyName("gravity")]
        public string Gravity { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("orbital_period")]
        public string OrbitalPeriod { get; set; }

        [JsonPropertyName("population")]
        public string Population { get; set; }

        [JsonPropertyName("residents")]
        public List<string> ResidentEndpoints { get; set; }

        [JsonPropertyName("rotation_period")]
        public string RotationPeriod { get; set; }

        [JsonPropertyName("surface_water")]
        public string SurfaceWater { get; set; }

        [JsonPropertyName("terrain")]
        public string Terrain { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(FilmEndpoints))]
        public List<Film> Films { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(ResidentEndpoints))]
        public List<Person> Residents { get; set; }
    }
}
