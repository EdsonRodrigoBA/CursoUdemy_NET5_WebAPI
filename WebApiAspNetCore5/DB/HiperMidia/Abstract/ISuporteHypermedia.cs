using System.Collections.Generic;

namespace WebApiAspNetCore5.DB.HiperMidia.Abstract
{
    public interface ISuporteHypermedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
