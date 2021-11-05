using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DBModels
{
    public class Agencia
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        [Required]
        public string Nome { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }
    }
}
