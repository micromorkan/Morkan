using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DBModels
{
    public class Cliente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        [Required]
        public string Nome { get; set; }

        [Column("cpf")]
        [Required]
        public string Cpf { get; set; }
        
        [Column("email")]
        public string Email { get; set; }

        [Column("telefone1")]
        public string Telefone1 { get; set; }

        [Column("telefone2")]
        public string Telefone2 { get; set; }
    }
}
