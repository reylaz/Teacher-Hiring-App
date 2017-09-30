using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeacherHiring.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeacherHiring.DataBase.Access;
using TeacherHiring.DataBase.Model;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuMaster : ContentPage
    {
        public ListView ListView;

        public MenuMaster()
        {
            InitializeComponent();

            BindingContext = new MenuMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<OpcionesMenuModel> MenuItems { get; set; }
            public UsuarioDataAccess usuarioDataAccess = new UsuarioDataAccess();
            public Usuario usuario = new Usuario();

            public MenuMasterViewModel()
            {
                usuario = usuarioDataAccess.GetUser();
                MenuItems = new ObservableCollection<OpcionesMenuModel>(OpcionesMenuMetodo.getOpcionesMenu(usuario.IdTipoPerson));
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}