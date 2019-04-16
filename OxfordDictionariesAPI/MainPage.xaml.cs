using OxfordDictionariesAPI.ViewModel;
using Xamarin.Forms;

namespace OxfordDictionariesAPI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(this);
        }
    }
}
