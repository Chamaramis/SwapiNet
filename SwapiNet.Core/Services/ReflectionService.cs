using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SwapiNet.Core.Attributes;
using SwapiNet.Core.Models;

namespace SwapiNet.Core.Services
{
    public class ReflectionService
    {
        private static ReflectionService _instance;

        private ReflectionService()
        {
        }

        public static ReflectionService Instance => _instance ??= new ReflectionService();

        public async Task LoadLazyPropertiesForResponse(Type responseType, object deserializedResponse)
        {
            var lazyProperties = responseType
                            .GetProperties()
                            .Where(p => p.GetCustomAttributes(true).Any(a => a is BaseLazyPropertyAttribute))
                            .Select(p => p)
                            .ToList();

            var propertyTasks = new List<Task>();

            foreach (var property in lazyProperties)
            {
                var baseAttribute = (BaseLazyPropertyAttribute)property.GetCustomAttributes(true)
                    .FirstOrDefault(a => a is BaseLazyPropertyAttribute);

                var endpointProperty = responseType
                    .GetProperties()
                    .FirstOrDefault(p => p.Name == baseAttribute?.EndpointPropertyName);

                if (endpointProperty is not null)
                {
                    if (baseAttribute is LazyValuePropertyAttribute valueProperty)
                    {
                        propertyTasks.Add(LoadLazyValueProperty(deserializedResponse, property, endpointProperty));
                    }
                    else if (baseAttribute is LazyCollectionPropertyAttribute collectionProperty)
                    {
                        propertyTasks.Add(LoadLazyCollectionProperty(deserializedResponse, property, endpointProperty));
                    }
                }
            }

            await Task.WhenAll(propertyTasks);
        }

        private async Task LoadLazyValueProperty(object deserializedResponse, PropertyInfo lazyProperty, PropertyInfo endpointProperty)
        {
            try
            {
                var lazyEndpoint = (string)endpointProperty.GetValue(deserializedResponse);

                if (!string.IsNullOrWhiteSpace(lazyEndpoint))
                {
                    Type lazyType = null;
                    foreach (var propertyTypeInterface in lazyProperty.PropertyType.GetInterfaces())
                    {
                        if (propertyTypeInterface.IsGenericType &&
                            propertyTypeInterface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                        {
                            lazyType = propertyTypeInterface.GetGenericArguments()[0];
                        }
                    }

                    var lazyResult = await BasicHttpClientService.Instance.Get(lazyType, lazyEndpoint, false, true);

                    lazyProperty.SetValue(deserializedResponse, lazyResult);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task LoadLazyCollectionProperty(object deserializedResponse, PropertyInfo lazyProperty, PropertyInfo endpointProperty)
        {
            try
            {
                var lazyEndpointsEnumerable = ((IEnumerable<string>)endpointProperty.GetValue(deserializedResponse));

                if (lazyEndpointsEnumerable is null)
                {
                    return;
                }

                var lazyEndpoints = lazyEndpointsEnumerable.ToList();
 
                if (lazyEndpoints.Any() &&
                    lazyEndpoints.All(e => !string.IsNullOrWhiteSpace(e)))
                {
                    var lazyTasks = new List<Task<object>>();

                    foreach (var lazyEndpoint in lazyEndpoints)
                    {
                        Type lazyType = null;
                        foreach (var propertyTypeInterface in lazyProperty.PropertyType.GetInterfaces())
                        {
                            if (propertyTypeInterface.IsGenericType &&
                                propertyTypeInterface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                            {
                                lazyType = propertyTypeInterface.GetGenericArguments()[0];
                            }
                        }


                        lazyTasks.Add(BasicHttpClientService.Instance.Get(lazyType, lazyEndpoint, false, true));
                    }

                    await Task.WhenAll(lazyTasks);

                    var collectionInstance = Activator.CreateInstance(lazyProperty.PropertyType);

                    var lazyResults = (IList)collectionInstance;

                    if (lazyResults is not null)
                    {
                        foreach (var lazyTask in lazyTasks)
                        {
                            var result = await lazyTask;
                            lazyResults.Add(result);
                        }

                        lazyProperty.SetValue(deserializedResponse, lazyResults);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
