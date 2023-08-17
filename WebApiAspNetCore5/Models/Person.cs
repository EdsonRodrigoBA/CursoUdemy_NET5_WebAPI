using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models.Base;

namespace WebApiAspNetCore5.Models
{
    [Table("person")]
    public class Person : BaseEntity
    {
     

        [Column("FirstName")]
        public String firstname { get; set; }

        [Column("LastName")]
        public String lastname { get; set; }

        [Column("Address")]
        public String address { get; set; }

        [Column("enabled")]
        public bool enabled { get; set; }

    }
}
