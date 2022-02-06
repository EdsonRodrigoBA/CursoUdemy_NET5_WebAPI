using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiAspNetCore5.Data.VO;
using WebApiAspNetCore5.DB.HiperMidia.Constants;

namespace WebApiAspNetCore5.DB.HiperMidia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(PersonVO conteudo, IUrlHelper urlHelper)
        {
            var path = "api/person/v1";
            String link = getLink(conteudo.id, urlHelper, path);


            conteudo.Links = new List<HyperMediaLink>();
            conteudo.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });
            conteudo.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            conteudo.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            conteudo.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });

            return Task.FromResult<object>(null);
        }

        private string getLink(long id, IUrlHelper urlHelper, String path)
        {
            lock (_lock)
            {
                var url = new
                {

                    controller = path,
                    id = id
                };

                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2f", "/").Replace("%2F", "/").ToString();
            }
        }



    }
}
