using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DBModels
{
    public class Servico
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        [Column("clienteid")]
        public int ClienteId { get; set; }

        [ForeignKey("Agencia")]
        [Column("agenciaid")]
        public int AgenciaId { get; set; }

        [Column("transferin")]
        public string TransferIN { get; set; }

        [Column("horariovoo")]
        public string HorarioVoo { get; set; }

        [Column("numerovoo")]
        public string NumeroVoo { get; set; }

        [Column("companhia")]
        public string Companhia { get; set; }

        [Column("datavoo")]
        public DateTime DataVoo { get; set; }

        [Column("saida")]
        public string Saida { get; set; }

        [Column("qtdpassageiros")]
        public string QtdPassageiros { get; set; }

        [Column("veiculo")]
        public string Veiculo { get; set; }

        [Column("observacao")]
        public string Observacao { get; set; }

        [Column("valor")]
        public string Valor { get; set; }

        [Column("dataservico")]
        public DateTime DataServico { get; set; }

        [Column("datacadastro")]
        public DateTime DataCadastro { get; set; }

        public Cliente Cliente { get; set; }

        public Agencia Agencia { get; set; }
    }
}
