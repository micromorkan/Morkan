using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.DBModels;

namespace Web.ViewModels
{
    public class ServicoVM
    {
        public int Id { get; set; }
        public string TransferIN { get; set; }
        public string HorarioVoo { get; set; }
        public string NumeroVoo { get; set; }
        public string Companhia { get; set; }
        public DateTime DataVoo { get; set; }
        public string Saida { get; set; }
        public string QtdPassageiros { get; set; }
        public string Veiculo { get; set; }
        public string Observacao { get; set; }
        public string Valor { get; set; }
        public DateTime DataServico { get; set; }
        public DateTime DataCadastro { get; set; }
        public Cliente Cliente { get; set; }
        public Agencia Agencia { get; set; }
    }
}
