using ProductOrg.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductOrg.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoPage : ContentPage
    {
        public ToDoPage()
        {
            InitializeComponent();
            BindingContext = new TodoViewModel(Navigation);
        }
    }
}