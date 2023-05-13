using ProductOrg.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductOrg.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PomodoroPage : ContentPage
    {
        public PomodoroPage()
        {
            InitializeComponent();
            BindingContext = new PomodoroViewModel(Navigation);
        }
    }
}