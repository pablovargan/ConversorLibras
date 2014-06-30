using Microsoft.Practices.Unity;
namespace ConversorLibrasWP81.ViewModels.Base
{
    public class ViewModelLocator
    {
        IUnityContainer container;

        public ViewModelLocator()
        {
            container = new UnityContainer();

            container.RegisterType<MainViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get { return container.Resolve<MainViewModel>(); }
        }
    }
}
