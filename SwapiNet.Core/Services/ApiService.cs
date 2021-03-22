using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwapiNet.Core.Models;

namespace SwapiNet.Core.Services
{
    public class ApiService
    {
        private static ApiService _instance;
        private Dictionary<Type, string> endpoints;

        private ApiService()
        {
            endpoints = new Dictionary<Type, string>();
            RegisterEndpoints();
        }

        public static ApiService Instance => _instance ??= new ApiService();

        private void RegisterEndpoints()
        {
            endpoints.Add(typeof(Film), ConfigurationService.Instance.Root.Films);
            endpoints.Add(typeof(Person), ConfigurationService.Instance.Root.People);
            endpoints.Add(typeof(Planet), ConfigurationService.Instance.Root.Planets);
            endpoints.Add(typeof(Species), ConfigurationService.Instance.Root.Species);
            endpoints.Add(typeof(Starship), ConfigurationService.Instance.Root.Starships);
            endpoints.Add(typeof(Vehicle), ConfigurationService.Instance.Root.Vehicles);
        }

        public async Task<ICollection<T>> GetAll<T>()
            where T : BaseModel
        {
            if (endpoints.TryGetValue(typeof(T), out string endpoint))
            {
                var response = await BasicHttpClientService.Instance.GetMany<T>(endpoint);
                return response;
            }

            return new List<T>(0);
        }

        public async Task<CollectionResponse<T>> GetByPageNumber<T>(int pageNumber)
            where T : BaseModel
        {
            if (endpoints.TryGetValue(typeof(T), out string endpoint))
            {
                var response = await BasicHttpClientService.Instance.GetManyByPageNumber<T>(endpoint, pageNumber);
                return response;
            }

            return default;
        }

        public async Task<T> GetById<T>(int id, bool loadLazyProperties = true)
            where T : BaseModel
        {
            if (endpoints.TryGetValue(typeof(T), out string endpoint))
            {
                var response = await BasicHttpClientService.Instance.Get<T>($"{endpoint}{id}", loadLazyProperties);
                return response;
            }

            return default;
        }
    }
}
