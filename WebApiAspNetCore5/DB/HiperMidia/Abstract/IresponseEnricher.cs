using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace WebApiAspNetCore5.DB.HiperMidia.Abstract
{
    public interface IresponseEnricher
    {

        bool CanEnrich(ResultExecutingContext resultExecutedContext);

        Task Enrich(ResultExecutingContext resultExecutedContext);

    }
}
