namespace ConversorLibrasWP81.ViewModels.Base
{
    using Autofac;
    using ConversorLibrasWP81.Services.Currency;
    using ConversorLibrasWP81.Services.Network;

    public class ViewModelLocator
    {
        IContainer container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NetworkService>().As<INetworkService>().SingleInstance();
            builder.RegisterType<CurrencyService>().As<ICurrencyService>().SingleInstance();

            builder.RegisterType<MainViewModel>();
            this.container = builder.Build();
        }

        public MainViewModel MainViewModel
        {
            get { return container.Resolve<MainViewModel>(); }
        }
    }
}
