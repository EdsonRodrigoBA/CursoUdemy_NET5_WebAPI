using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.DB.HiperMidia.Abstract;

namespace WebApiAspNetCore5.DB.HiperMidia
{
    public abstract class ContentResponseEnricher<T> : IresponseEnricher where T : ISuporteHypermedia
    {
        public ContentResponseEnricher()
        {

        }
        public bool canEnrich(Type ContentType)
        {
            return ContentType == typeof(T) || ContentType == typeof(List<T>);
        }

        protected abstract Task EnrichModel(T conteudo, IUrlHelper urlHelper);

        bool IresponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
            {
                return canEnrich(okObjectResult.Value.GetType());
            }
            return false;
        }

        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
            if (response.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> colection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(colection);
                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
            }
      

            await Task.FromResult<Object>(null);

        }
    }
}
