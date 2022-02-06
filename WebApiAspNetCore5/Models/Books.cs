using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models.Base;

namespace WebApiAspNetCore5.Models
{
    [Table("books")]
    public class Books : BaseEntity
    {
      

        [Column("descricao")]
        [Required(ErrorMessage = "A descrição é Obrigatória")]

        public String descricao { get; set; }

        [Column("titulo")]
        [Required(ErrorMessage = "O Titulo é Obrigatório.")]
        public String titulo { get; set; }


        [Column("autor")]
        [Required(ErrorMessage = "O Autor é Obrigatório.")]

        public String autor { get; set; }


        [Column("data_publicacao")]
        [Required(ErrorMessage = "O data de publicação é Obrigatório.")]

        public DateTime data_publicacao { get; set; }
    }
}
