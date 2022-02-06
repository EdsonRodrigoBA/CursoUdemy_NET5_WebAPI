using System;

namespace WebApiAspNetCore5.Configuration
{
    public class TokenConfiguration
    {
        public String audience { get; set; }

        public String issuer { get; set; }

        public String secrete { get; set; }

        public int minutes { get; set; }

        public int DaysToExpiry { get; set; }



    }
}
