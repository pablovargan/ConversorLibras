namespace ConversorLibrasWP81.Services.Currency
{
    using System.Threading.Tasks;
    public interface ICurrencyService
    {
        Task<string> GetPoundCurrencyAsync();
    }
}
