using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiAspNetCore5.DB.HiperMidia
{
    public class HyperMediaLink
    {
        public String Rel { get; set; }
        public String Href
        {
            get
            {
                object _lock = new object();
                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2f", "/").Replace("%2F", "/").ToString();
                }
            }

            set
            {
                href = value;
            }

        }

        private String href;

        public String Type { get; set; }

        public String Action { get; set; }

    }
}
