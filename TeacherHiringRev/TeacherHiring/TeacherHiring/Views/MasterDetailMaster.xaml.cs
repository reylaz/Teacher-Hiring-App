using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherHiring.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailMaster : ContentPage
    {
        public ListView ListView;
        MasterPageViewModel viewModel;
        public MasterDetailMaster()
        {
            InitializeComponent();
            BindingContext = viewModel = new MasterPageViewModel();
            ListView = MenuItemListView;
            SignOutText.GestureRecognizers.Add(new TapGestureRecognizer { Command = viewModel.SignOut });
        }
    }
}