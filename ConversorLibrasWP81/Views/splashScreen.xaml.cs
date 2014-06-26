namespace ConversorLibrasWP81.Views
{
    using ConversorLibrasWP81.Views.Base;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class SplashScreen : PageBase
    {
        public SplashScreen()
        {
            this.InitializeComponent();
            Splash_Screen();
        }
        async void Splash_Screen()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
