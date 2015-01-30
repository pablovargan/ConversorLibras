namespace ConversorLibrasWP81.ViewModels
{
    using ConversorLibrasWP81.Models;
    using ConversorLibrasWP81.Services.Currency;
    using ConversorLibrasWP81.Services.Network;
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
        private readonly INetworkService networkService;
        private readonly ICurrencyService currencyService;

        private string from;
        private string euro;
        private string to;
        private Money money;

        // Commands
        private DelegateCommand convertCommand;
        private DelegateCommandAsync rateCommand;

        public override async Task OnNavigatedTo(NavigationEventArgs args)
        {
            this.To = "- €";
            if (networkService.IsOnline())
            {
                var response = await this.currencyService.GetPoundCurrencyAsync();
                this.money = JsonConvert.DeserializeObject<Money>(response);
                GoogleAnalytics.EasyTracker.GetTracker().SendView("MainPage");
            }
        }
        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatingFrom(NavigatingCancelEventArgs args)
        {
            return null;
        }

        public MainViewModel(INetworkService networkService, ICurrencyService currencyService) 
        {
            this.money = new Money() { Rate = 0.8440 };

            this.networkService = networkService;
            this.currencyService = currencyService;
            
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
                this.From = string.Empty;
                // Conversion y redondeo a 2 decimales
                double result = Math.Round((value * 1 / this.money.Rate), 2);
                
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
    }
}
