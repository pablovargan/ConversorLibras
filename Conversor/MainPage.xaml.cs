using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Conversor.Resources;

using System.Windows.Media.Imaging;
using System.Globalization;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
// Estado de internet
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Tasks;
using System.Reflection;

namespace Conversor
{
    public class Moneda
    {
        public Moneda() { }
        public string To { get; set; }
        public double Rate { get; set; }
        public string From { get; set; }
        public string V { get; set; }
    }

    public partial class MainPage : PhoneApplicationPage
    {
        // URL para hacer la peticion GET
        private static string urlRequest = "http://rate-exchange.appspot.com/currency?from=EUR&to=GBP&q=1";
        private Moneda _moneda;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // Código de ejemplo para traducir ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            FlurryWP8SDK.Api.SetVersion("1.7");
            FlurryWP8SDK.Api.LogEvent("Aplicacion iniciada");

            _moneda = new Moneda() { Rate = 0.8440 };
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                // Realizo la descarga desde la web para recoger el Json
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += webClient_DownloadStringAsync;
                webClient.DownloadStringAsync(new Uri(urlRequest));
            }
        }

        public void webClient_DownloadStringAsync(object sender, DownloadStringCompletedEventArgs e)
        {
            if(!e.Cancelled && e.Error != null)
            {
                if (!string.IsNullOrWhiteSpace(e.Result as string))
                {
                    // Parse del resultado obtenido (unico)
                    var resultado = JsonConvert.DeserializeObject<Moneda>(e.Result);
                    // Lo asigno a la moneda que tenemos
                    this._moneda = resultado;
                }
            }
        }

        // Evento para realizar la conversion de euros a libras
        private void Convertir_Click(object sender, RoutedEventArgs e)
        {
            // Si es distinto de nulo o un espacio en blanco. Mejor que != "" <-- mas robusto
            if(!String.IsNullOrWhiteSpace(this.Datos.Text))
            {
                try
                {
                    // El string a interpretar esta en latino--> 12,23
                    double valor;
                    // TryParse(String, NumberStyle, IFormatProvider, Double) 
                    /* Convierte en forma de cadena un numero con estilo y formato en un numero
                     * de punto flotante de doble equivalente con el formato que le hemos dicho
                     */
                    CultureInfo aux = new CultureInfo(CultureInfo.CurrentCulture.ToString());
                    Double.TryParse(this.Datos.Text, System.Globalization.NumberStyles.Currency, new CultureInfo("es-ES"), out valor); //Para el futuro converson serviria para el punto en-US
                    // Vuelvo a dejar el textbox a vacio
                    this.Datos.Text = string.Empty;
                    // Conversion y redondeo a 2 decimales
                    double resultado = Math.Round((valor * 1 / _moneda.Rate), 2);

                    // Se asigna al string el tipo de moneda que queremos en la salida y asi es controlado por el sistema
                    // Libras
                    var stringResultadoLibras = valor.ToString("C", new CultureInfo("en-GB")) + " =";
                    this.Libras.Text = stringResultadoLibras;
                    // Euros
                    var stringResultadoEuros = resultado.ToString("C", new CultureInfo("es-ES"));
                    this.Resultado.Text = stringResultadoEuros;
                }
                catch (FormatException fe)
                {
                    this.Datos.Text = string.Empty;
                }
                finally
                {
                    // Notifico que se ha realizado la conversion
                    FlurryWP8SDK.Api.LogEvent("Conversion realizada");
                }
            }
        }

        // Evento para lanzar el market y valorar la app
        private void RateApp_Click(object sender, EventArgs e)
        {
            FlurryWP8SDK.Api.LogEvent("Evento de valorar la app");
            MarketplaceReviewTask rate = new MarketplaceReviewTask();
            rate.Show();
        }

        //Para que no vuelva al splash screen y termine
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Terminate();
        }
    }
}