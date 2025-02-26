using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using CotDolar.Service.Model;
using System.Net;
using static CotDolar.Service.Model.ModDolar;

namespace CotDolar.Service
{
    public class SerBcbDolar
    {
        private static string cotUrl = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoDolarPeriodo(dataInicial=@dataInicial,dataFinalCotacao=@dataFinalCotacao)?@dataInicial='{0}-{1}-{2}'&@dataFinalCotacao='{3}-{4}-{5}'&$top=1&$orderby=dataHoraCotacao%20desc&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

        public static ModDolar exibirCot(string dtInicial = null, string dtFinal = null)
        {
            if (dtInicial != null && dtFinal != null)
            {
                string[] dtI = dtInicial.Split('-');
                string diaI = dtI[0];
                string mesI = dtI[1];
                string anoI = dtI[2];

                string[] dtF = dtFinal.Split('-');
                string diaF = dtF[0];
                string mesF = dtF[1];
                string anoF = dtF[2];

                string novaCotUrl = string.Format(cotUrl, mesI, diaI, anoI, mesF, diaF, anoF);

                WebClient wc = new WebClient();
                string conteudo = wc.DownloadString(novaCotUrl);

                RespostaApi response = JsonConvert.DeserializeObject<RespostaApi>(conteudo);

                if (response.Value != null && response.Value.Count > 0)
                {
                    CotacaoDoDolar cotacao = response.Value[0];

                    return new ModDolar
                    {
                        CotCompra = cotacao.CotacaoCompra.ToString("F4"),
                        CotVenda = cotacao.CotacaoVenda.ToString("F4"),
                        DataHoraCot = cotacao.DataHoraCotacao
                    };
                }

                return null;
            }
            else
            {
                DateTime dtAt = DateTime.Now;
                DateTime dtSem = dtAt.AddDays(-7);

                string novaCotUrl = string.Format(cotUrl, dtSem.Month.ToString("D2"), dtSem.Day.ToString("D2"), dtSem.Year.ToString(), dtAt.Month.ToString("D2"), dtAt.Day.ToString("D2"), dtAt.Year.ToString());

                WebClient wc = new WebClient();
                string conteudo = wc.DownloadString(novaCotUrl);

                RespostaApi response = JsonConvert.DeserializeObject<RespostaApi>(conteudo);

                if (response.Value != null && response.Value.Count > 0)
                {
                    CotacaoDoDolar cotacao = response.Value[0];

                    return new ModDolar
                    {
                        CotCompra = cotacao.CotacaoCompra.ToString("F4"),
                        CotVenda = cotacao.CotacaoVenda.ToString("F4"),
                        DataHoraCot = cotacao.DataHoraCotacao
                    };
                }

                return null;
            }
        }
    }
}
