using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schad.Web.Maps
{
    public interface IMap<in I, out O>
    {
        O ToMap(I source);
    }
}
