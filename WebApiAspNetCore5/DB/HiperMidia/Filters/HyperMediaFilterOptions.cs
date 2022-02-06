using System.Collections.Generic;
using WebApiAspNetCore5.DB.HiperMidia.Abstract;

namespace WebApiAspNetCore5.DB.HiperMidia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IresponseEnricher> ContentResponseEnrichers { get; set; } = new List<IresponseEnricher>();
    }
}
