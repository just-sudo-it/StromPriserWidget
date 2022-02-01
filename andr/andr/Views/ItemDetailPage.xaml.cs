using andr.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace andr.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}