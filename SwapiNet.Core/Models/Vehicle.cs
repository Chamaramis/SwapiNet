using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SwapiNet.Core.Attributes;

namespace SwapiNet.Core.Models
{
    public class Vehicle : BaseModel
    {
        [JsonPropertyName("cargo_capacity")]
        public string CargoCapacity { get; set; }

        [JsonPropertyName("consumables")]
        public string Consumables { get; set; }

        [JsonPropertyName("cost_in_credits")]
        public string CostInCredits { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("crew")]
        public string Crew { get; set; }

        [JsonPropertyName("edited")]
        public DateTime Edited { get; set; }

        [JsonPropertyName("length")]
        public string Length { get; set; }

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonPropertyName("max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("passengers")]
        public string Passengers { get; set; }

        [JsonPropertyName("pilots")]
        public List<string> PilotEndpoints { get; set; }

        [JsonPropertyName("films")]
        public List<string> FilmEndpoints { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("vehicle_class")]
        public string VehicleClass { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(FilmEndpoints))]
        public List<Film> Films { get; set; }

        [JsonIgnore]
        [LazyCollectionProperty(nameof(PilotEndpoints))]
        public List<Person> Pilots { get; set; }
    }
}
