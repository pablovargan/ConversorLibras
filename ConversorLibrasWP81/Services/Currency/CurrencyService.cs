namespace ConversorLibrasWP81.Services.Currency
{
    using System;
    using System.Threading.Tasks;
using Windows.Web.Http;

    public class CurrencyService : ICurrencyService
    {
        private string urlCurrency = "http://rate-exchange.appspot.com/currency?from=EUR&to=GBP&q=1";
        public async Task<string> GetPoundCurrencyAsync()
        {
            using(HttpClient httpClient = new HttpClient()) 
            {
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(this.urlCurrency));
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
