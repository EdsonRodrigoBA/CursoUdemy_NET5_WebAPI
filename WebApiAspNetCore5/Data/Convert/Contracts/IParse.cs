using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAspNetCore5.Data.Convert.Contracts
{
    public interface IParse<O, D>
    {

        D Parse(O origem);
        List<D> Parse(List<O> origem);

    }
}
