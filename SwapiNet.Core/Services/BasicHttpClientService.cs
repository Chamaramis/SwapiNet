using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SwapiNet.Core.Attributes;
using SwapiNet.Core.Models;

namespace SwapiNet.Core.Services
{
    public class BasicHttpClientService
    {
        private static BasicHttpClientService _instance;

        private BasicHttpClientService()
        {
        }

        public static BasicHttpClientService Instance => _instance ??= new BasicHttpClientService();

        public async Task<T> Get<T>(string endpoint, bool loadLazyProperties = true)
            where T : BaseModel
        {
            var response = await Get(typeof(T), endpoint, loadLazyProperties);
            return response as T;
        }

        public async Task<object> Get(Type responseType, string endpoint, bool loadLazyProperties = true, bool overrideBaseUrl = false)
        {
            try
            {
                var httpResponse = await ExecuteHttpRequest(endpoint, overrideBaseUrl);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var deserializedResponse =
                        await System.Text.Json.JsonSerializer.DeserializeAsync(
                            await httpResponse.Content.ReadAsStreamAsync(),
                            responseType);

                    if (loadLazyProperties)
                    {
                        await ReflectionService.Instance.LoadLazyPropertiesForResponse(responseType,
                            deserializedResponse);
                    }

                    return deserializedResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default;
        }

        public async Task<ICollection<T>> GetMany<T>(string endpoint, bool loadLazyProperties = false)
            where T : BaseModel
        {
            try
            {
                int pageNumber = 1;
                var results = new List<T>();
                var deserializedResponse = new CollectionResponse<T>();
                do
                {
                    deserializedResponse = await GetManyByPageNumber<T>(endpoint, pageNumber, loadLazyProperties);

                    if (deserializedResponse is not null &&
                        deserializedResponse.Results?.Any() == true)
                    {
                        results.AddRange(deserializedResponse.Results);
                    }

                    pageNumber++;
                } 
                while (!string.IsNullOrWhiteSpace(deserializedResponse?.Next?.ToString()));

                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default;
        }

        public async Task<CollectionResponse<T>> GetManyByPageNumber<T>(string endpoint, int pageNumber, bool loadLazyProperties = false)
            where T : BaseModel
        {
            try
            {
                var httpResponse = await ExecuteHttpRequest($"{endpoint}?page={pageNumber}", loadLazyProperties);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var deserializedResponse =
                        await System.Text.Json.JsonSerializer.DeserializeAsync<CollectionResponse<T>>(
                            await httpResponse.Content.ReadAsStreamAsync());

                    return deserializedResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default;
        }

        private async Task<HttpResponseMessage> ExecuteHttpRequest(string endpoint, bool overrideBaseUrl = false)
        {
            using var httpClient = new HttpClient();

            if (!overrideBaseUrl)
            {
                httpClient.BaseAddress = new Uri(ConfigurationService.Instance.BaseSwapiUrl);
            }
            
            var httpResponse = await httpClient.GetAsync(endpoint);

            if ((httpResponse.StatusCode == HttpStatusCode.Moved ||
                 httpResponse.StatusCode == HttpStatusCode.MovedPermanently) &&
                !string.IsNullOrWhiteSpace(httpResponse.Headers?.Location?.AbsoluteUri))
            {
                httpResponse = await ExecuteHttpRequest(httpResponse.Headers.Location.AbsoluteUri, true);
            }

            return httpResponse;
        }
    }
}
