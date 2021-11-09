using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.DBModels;

namespace Web.ViewModels
{
    public class ServicoVM
    {
        public DateTime FiltroDataDe { get; set; }
        public DateTime FiltroDataAte { get; set; }
        public IEnumerable<Servico> Servicos { get; set; }
    }
}
