using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAspNetCore5.DB.HiperMidia.Filters
{
    public class HyperMidiaFilter : ResultFilterAttribute
    {

        private readonly HyperMediaFilterOptions _hyperMediaFilterOptions;

        public HyperMidiaFilter(HyperMediaFilterOptions hyperMediaFilterOptions)
        {
            _hyperMediaFilterOptions = hyperMediaFilterOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult objectResult)
            {
                var enricher = _hyperMediaFilterOptions
                    .ContentResponseEnrichers
                    .FirstOrDefault(x => x.CanEnrich(context));
                 if (enricher != null) Task.FromResult(enricher.Enrich(context)); 
            };
        }
    }
}
