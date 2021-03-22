using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapiNet.Core.Attributes
{
    public class BaseLazyPropertyAttribute : Attribute
    {
        public BaseLazyPropertyAttribute(string endpointPropertyName)
        {
            EndpointPropertyName = endpointPropertyName;
        }

        public string EndpointPropertyName { get; set; }
    }
}
