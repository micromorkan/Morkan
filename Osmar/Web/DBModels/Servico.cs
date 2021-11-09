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

        #region IN

        [Column("transferin")]
        public string TransferIN { get; set; }

        [Column("horariovooin")]
        public string HorarioVooIn { get; set; }

        [Column("numerovooin")]
        public string NumeroVooIn { get; set; }

        [Column("companhiain")]
        public string CompanhiaIn { get; set; }

        [Column("datavoo")]
        public DateTime DataVooIn { get; set; }

        #endregion

        #region OUT

        [Column("transferout")]
        public string TransferOut { get; set; }

        [Column("horariovooout")]
        public string HorarioVooOut { get; set; }

        [Column("numerovooout")]
        public string NumeroVooOut { get; set; }

        [Column("companhiaout")]
        public string CompanhiaOut { get; set; }

        [Column("datavooout")]
        public DateTime DataVooOut { get; set; }

        [Column("saidahotelout")]
        public string SaidaHotelOut { get; set; }

        #endregion

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
