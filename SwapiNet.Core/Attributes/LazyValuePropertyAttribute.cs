using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapiNet.Core.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class LazyValuePropertyAttribute : BaseLazyPropertyAttribute
    {
        public LazyValuePropertyAttribute(string endpointPropertyName) : base(endpointPropertyName)
        {
        }
    }
}
