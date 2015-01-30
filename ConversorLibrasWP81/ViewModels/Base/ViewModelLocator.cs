namespace ConversorLibrasWP81.ViewModels.Base
{
    using Autofac;

    public class ViewModelLocator
    {
        IContainer container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainViewModel>();
            this.container = builder.Build();
        }

        public MainViewModel MainViewModel
        {
            get { return container.Resolve<MainViewModel>(); }
        }
    }
}
