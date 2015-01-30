namespace ConversorLibrasWP81.ViewModels
{
    using ConversorLibrasWP81.Common;
    using ConversorLibrasWP81.Models;
    using ConversorLibrasWP81.ViewModels.Base;
    using Newtonsoft.Json;
    using System;
    using System.Globalization;
    using System.Net.NetworkInformation;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.ApplicationModel.Store;
    using Windows.UI.Xaml.Navigation;
    using Windows.Web.Http;

    public class MainViewModel : ViewModelBase
    {
        // Services
        private Money _money;

        private string from;
        private string euro;
        private string to;

        // Commands
        private DelegateCommand convertCommand;
        private DelegateCommandAsync rateCommand;

        public override async Task OnNavigatedTo(NavigationEventArgs args)
        {
            this.To = "- €";
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var responseText = await DownloadCurrencyAsync();
                _money = JsonConvert.DeserializeObject<Money>(responseText);
            }

            GoogleAnalytics.EasyTracker.GetTracker().SendView("MainPage");
        }
        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatingFrom(NavigatingCancelEventArgs args)
        {
            return null;
        }

        public MainViewModel() 
        {
            _money = new Money() { Rate = 0.8440 };
            this.convertCommand = new DelegateCommand(ConvertExecute, CanConvertExecute);
            this.rateCommand = new DelegateCommandAsync(RateExecute);
        }

        public string From
        {
            get { return this.from; }
            set
            {
                this.from = value;
                RaisePropertyChanged();
                this.convertCommand.RaiseCanExecuteChanged();
            }
        }

        public string Euro
        {
            get { return this.euro; }
            set
            {
                this.euro = value;
                RaisePropertyChanged();
            }
        }

        public string To
        {
            get { return this.to; }
            set
            {
                this.to = value;
                RaisePropertyChanged();
            }
        }
        public ICommand RateCommand
        {
            get { return this.rateCommand; }
        }

        public ICommand ConvertCommand
        {
            get { return this.convertCommand; }
        }
        private bool CanConvertExecute()
        {
            return !string.IsNullOrWhiteSpace(this.from);
        }

        private void ConvertExecute()
        {
            try
            {
                double value;
                CultureInfo aux = new CultureInfo(CultureInfo.CurrentCulture.ToString());
                Double.TryParse(this.From, System.Globalization.NumberStyles.Currency, new CultureInfo("es-ES"), out value); 
                // Vuelvo a dejar el textbox a vacio
                this.From = string.Empty;
                // Conversion y redondeo a 2 decimales
                double result = Math.Round((value * 1 / _money.Rate), 2);

                // Se asigna al string el tipo de moneda que queremos en la salida y asi es controlado por el sistema
                // Libras
                var stringResultLibra = value.ToString("C", new CultureInfo("en-GB")) + " =";
                this.Euro = stringResultLibra;
                // Euros
                var stringResultEuro = result.ToString("C", new CultureInfo("es-ES"));
                this.To = stringResultEuro;
            }
            catch (FormatException)
            {
                this.From = string.Empty;
            }
        }

        private async Task RateExecute()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }

        private async Task<string> DownloadCurrencyAsync()
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(new Uri(CommonKeys.urlCurrency));
            return await response.Content.ReadAsStringAsync();
        }
    }
}
