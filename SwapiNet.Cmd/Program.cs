using System;
using System.Threading.Tasks;
using SwapiNet.Core.Models;
using SwapiNet.Core.Services;

namespace SwapiNet.Cmd
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ConfigurationService.InitializeAsync();


            // todas las colecciones
            
            //var filmsTask = ApiService.Instance.GetAll<Film>();
            //var peopleTask = ApiService.Instance.GetAll<Person>();
            //var planetsTask = ApiService.Instance.GetAll<Planet>();
            //var speciesTask = ApiService.Instance.GetAll<Species>();
            //var starshipsTask = ApiService.Instance.GetAll<Starship>();
            //var vehiclesTask = ApiService.Instance.GetAll<Vehicle>();

            //await Task.WhenAll(filmsTask, peopleTask, planetsTask, speciesTask, starshipsTask, vehiclesTask);

            //var films = await filmsTask;
            //Console.WriteLine($"films: {films.Count}");

            //var people = await peopleTask;
            //Console.WriteLine($"people: {people.Count}");

            //var planets = await planetsTask;
            //Console.WriteLine($"planets: {planets.Count}");

            //var species = await speciesTask;
            //Console.WriteLine($"species: {species.Count}");

            //var starships = await starshipsTask;
            //Console.WriteLine($"starships: {starships.Count}");

            //var vehicles = await vehiclesTask;
            //Console.WriteLine($"vehicles: {vehicles.Count}");


            //Uno por uno
            
            var date = DateTime.Now;
            var film = await ApiService.Instance.GetById<Film>(1);
            Console.WriteLine($"Got film at {(DateTime.Now - date).TotalSeconds} seconds");
            date = DateTime.Now;
            var person = await ApiService.Instance.GetById<Person>(1);
            Console.WriteLine($"Got person at {(DateTime.Now - date).TotalSeconds} seconds");
            date = DateTime.Now;
            var planet = await ApiService.Instance.GetById<Planet>(1);
            Console.WriteLine($"Got planet at {(DateTime.Now - date).TotalSeconds} seconds");
            date = DateTime.Now;
            var species = await ApiService.Instance.GetById<Species>(1);
            Console.WriteLine($"Got species at {(DateTime.Now - date).TotalSeconds} seconds");
            date = DateTime.Now;
            var starship = await ApiService.Instance.GetById<Starship>(1);
            Console.WriteLine($"Got starship at {(DateTime.Now - date).TotalSeconds} seconds");
            date = DateTime.Now;
            var vehicle = await ApiService.Instance.GetById<Vehicle>(1);
            Console.WriteLine($"Got vehicle at {(DateTime.Now - date).TotalSeconds} seconds");
            date = DateTime.Now;
            Console.WriteLine("todo chido");
        }
    }
}
