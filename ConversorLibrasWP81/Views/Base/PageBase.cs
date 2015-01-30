namespace ConversorLibrasWP81.Views.Base
{
    using ConversorLibrasWP81.ViewModels.Base;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Xaml.Navigation;

    public class PageBase : Page
    {
        private ViewModelBase vm;

        public PageBase()
        {
            Transitions = new TransitionCollection();
            var theme = new NavigationThemeTransition
            {
                DefaultNavigationTransitionInfo = new SlideNavigationTransitionInfo()
            };
            Transitions.Add(theme);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            vm = (ViewModelBase)this.DataContext;
            vm.SetAppFrame(this.Frame);
            vm.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            vm.OnNavigatedFrom(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            vm.OnNavigatingFrom(e);
        }
    }
}
