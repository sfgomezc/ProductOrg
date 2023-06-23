using ProductOrg.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductOrg.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoItemPage : ContentPage
    {
        public ToDoItemPage()
        {
            InitializeComponent();
            BindingContext = new TodoItemViewModel(Navigation);
        }
    }
}