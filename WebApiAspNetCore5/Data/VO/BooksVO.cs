using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.DB.HiperMidia;
using WebApiAspNetCore5.DB.HiperMidia.Abstract;
using WebApiAspNetCore5.Models.Base;

namespace WebApiAspNetCore5.Models
{
    public class BooksVO : ISuporteHypermedia
    {



        public long id { get; set; }

        public String descricao { get; set; }

        public String titulo { get; set; }

        public String autor { get; set; }

        public DateTime data_publicacao { get; set; }
        public List<HyperMediaLink> Links { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
