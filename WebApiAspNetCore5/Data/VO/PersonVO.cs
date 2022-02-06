using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebApiAspNetCore5.DB.HiperMidia;
using WebApiAspNetCore5.DB.HiperMidia.Abstract;

namespace WebApiAspNetCore5.Data.VO
{

    public class PersonVO : ISuporteHypermedia
    {

        //Cutsom Serializable , customiza estrutura do Json
        //[JsonPropertyName("code")]
        public long id { get; set; }


        //[JsonPropertyName("name")]

        public String firstname { get; set; }

        //[JsonPropertyName("last_name")]

        public String lastname { get; set; }

        //[JsonPropertyName("Sex")]

        public String address { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
