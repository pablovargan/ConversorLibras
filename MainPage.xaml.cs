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

        private void Convertir_Click(object sender, RoutedEventArgs e)
        {
            if (this.Datos.Text != "")
            {
                try
                {
                    char[] numero = this.Datos.Text.ToCharArray();
                    //Busco la coma
                    int coma = this.Datos.Text.IndexOf('.');
                    if (coma != -1)
                    {
                        numero[coma] = ',';
                    }
                    string corregido = new string(numero);
                    double valor = Convert.ToDouble(corregido);
                    this.Datos.Text = "";
                    this.Libras.Text = valor.ToString() + "£ =";
                    double resultado = (valor * 1) / 0.826944;
                    this.Resultado.Text = Math.Round(resultado, 2).ToString() + "€";
                }
                catch (FormatException fe)
                {
                    this.Datos.Text = "";
                }
                
            }
            
        }

        //Para que no vuelva al splash screen y termine
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Terminate();
        }
        // Código de ejemplo para compilar una ApplicationBar traducida
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Establecer ApplicationBar de la página en una nueva instancia de ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crear un nuevo botón y establecer el valor de texto en la cadena traducida de AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crear un nuevo elemento de menú con la cadena traducida de AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}