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

namespace Conversor
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Código de ejemplo para traducir ApplicationBar
            //BuildLocalizedApplicationBar();
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
                    MessageBox.Show(aux.ToString());
                    Double.TryParse(this.Datos.Text, System.Globalization.NumberStyles.Currency, new CultureInfo("es-ES"), out valor); //Para el futuro converson serviria para el punto en-US
                    // Vuelvo a dejar el textbox a vacio
                    this.Datos.Text = string.Empty;
                    // Conversion y redondeo a 2 decimales
                    double resultado = Math.Round((valor * 1 / 0.83440), 2);

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
                
            }
            
        }

        //Para que no vuelva al splash screen y termine
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Terminate();
        }
    }
}