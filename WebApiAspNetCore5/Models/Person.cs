using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAspNetCore5.Models
{
    [Table("person")]
    public class Person
    {
        [Column("Id")]
        public long id { get; set; }

        [Column("FirstName")]
        public String firstname { get; set; }

        [Column("LastName")]
        public String lastname { get; set; }

        [Column("Address")]
        public String address { get; set; }

    }
}
