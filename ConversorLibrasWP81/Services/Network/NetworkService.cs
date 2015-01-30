namespace ConversorLibrasWP81.Services.Network
{
    using Windows.Networking.Connectivity;
    public class NetworkService : INetworkService
    {
        public bool IsOnline()
        {
            var internetProfile = NetworkInformation.GetInternetConnectionProfile();
            return internetProfile != null &&
                internetProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess; 
        }
    }
}
