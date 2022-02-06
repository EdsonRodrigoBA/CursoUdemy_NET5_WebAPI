using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAspNetCore5.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long id { get; set; }
    }
}
