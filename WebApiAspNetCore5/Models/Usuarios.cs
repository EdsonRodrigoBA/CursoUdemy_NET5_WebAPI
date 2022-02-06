using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAspNetCore5.Models
{
    [Table("Usuarios")]
    public class Usuarios
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Informe o nome de usuario")]
        public String user_name { get; set; }


        [Required(ErrorMessage = "Informe a senha")]
        public String password { get; set; }
        public String nome { get; set; }
        public String Role { get; set; }
        public String refresh_token { get; set; }
        public DateTime refresh_token_expire_time { get; set; }


    }
}
