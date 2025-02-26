using System;
using System.Collections.Generic;
using System.Text;

namespace CotDolar.Service.Model
{
    public class ModDolar
    {
        public string CotCompra { get; set; }
        public string CotVenda { get; set; }
        public string DataHoraCot { get; set; }
    }

    public class CotacaoDoDolar
    {
        public double CotacaoCompra { get; set; }
        public double CotacaoVenda { get; set; }
        public string DataHoraCotacao { get; set; }
    }

    public class RespostaApi
    {
        public List<CotacaoDoDolar> Value { get; set; }
    }
}
