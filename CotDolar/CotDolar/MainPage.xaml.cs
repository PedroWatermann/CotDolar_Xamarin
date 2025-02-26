using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CotDolar.Service;
using CotDolar.Service.Model;

namespace CotDolar
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ModDolar dolar = SerBcbDolar.exibirCot();

            lblCotCompra.Text = "R$" + dolar.CotCompra;
            lblCotVenda.Text = "R$" + dolar.CotVenda;

            string[] dthr = dolar.DataHoraCot.Split(' ');

            DateTime dtConv = DateTime.ParseExact(dthr[0], "yyyy-MM-dd", null);
            string dt = dtConv.ToString("dd/MM/yyyy");
            lblData.Text = "Data: " + dt;

            DateTime hrConv = DateTime.ParseExact(dthr[1], "HH:mm:ss.fff", null);
            string hr = hrConv.ToString("HH:mm:ss");
            lblHora.Text = "Hora: " + hr;

            dtpFiltro.Date = Convert.ToDateTime(dthr[0]);
            dtpFiltro.MaximumDate = DateTime.Today;
        }

        private void btnFiltrar_Clicked(object sender, EventArgs e)
        {
            string dataFinal = dtpFiltro.Date.ToString("dd-MM-yyyy");
            DateTime dataI = dtpFiltro.Date.AddDays(-7);
            string dataInicial = dataI.ToString("dd-MM-yyyy");

            ModDolar dolar = SerBcbDolar.exibirCot(dataInicial, dataFinal);

            lblCotCompra.Text = "R$" + dolar.CotCompra;
            lblCotVenda.Text = "R$" + dolar.CotVenda;

            string[] dthr = dolar.DataHoraCot.Split(' ');

            DateTime dtConv = DateTime.ParseExact(dthr[0], "yyyy-MM-dd", null);
            string dt = dtConv.ToString("dd/MM/yyyy");
            lblData.Text = "Data: " + dt;

            string horaComMilisegundos = dthr[1];
            if (horaComMilisegundos.Length == 8) 
            {
                horaComMilisegundos += ".000";
            } 
            else if (horaComMilisegundos.Length == 9)
            {
                horaComMilisegundos += "000";
            }
            else if (horaComMilisegundos.Length == 10)
            {
                horaComMilisegundos += "00";
            }
            else if (horaComMilisegundos.Length == 11)
            {
                horaComMilisegundos += "0";
            }

            DateTime hrConv = DateTime.ParseExact(horaComMilisegundos, "HH:mm:ss.fff", null, System.Globalization.DateTimeStyles.None);

            if (hrConv == DateTime.MinValue)
            {
                hrConv = DateTime.ParseExact(dthr[1], "HH:mm:ss", null, System.Globalization.DateTimeStyles.None);
            }

            string hr = hrConv.ToString("HH:mm:ss");
            lblHora.Text = "Hora: " + hr;
        }

        private void btnSair_Clicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch
            {

            }
        }
    }
}
