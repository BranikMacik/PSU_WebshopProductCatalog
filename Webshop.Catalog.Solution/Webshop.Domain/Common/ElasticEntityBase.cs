using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Webshop.Domain.Common
{
    public class ElasticEntityBase : Entity
    {
        protected ElasticEntityBase(string index, string type)
        {
            Index = index;
            Type = type;
        }

        [Ignore] public string Index { get; }
        [Ignore] public string Type { get; }

    }
}
