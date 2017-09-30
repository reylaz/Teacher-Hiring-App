using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherHiring.Views;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    public class MasterPageViewModel:BaseViewModel
    {
        public ObservableCollection<MasterPageMenuItem> MenuItems { get; set; }
        public ICommand SignOut { get; set; }


        public MasterPageViewModel()
        {
            MenuItems = new ObservableCollection<MasterPageMenuItem>(new[]
            {

                new MasterPageMenuItem(){Id=0,Title="Inicio",TargetType = typeof(WelcomePage),IconPath="ic_view_dashboard.png" },
                new MasterPageMenuItem(){Id=4,Title="Informacion",TargetType = typeof(InformacionPage),IconPath="ic_view_dashboard.png" },
                new MasterPageMenuItem(){Id=1,Title="Asesorias Programadas",TargetType = typeof(AsesoriasPage),IconPath="ic_view_dashboard.png" },
                new MasterPageMenuItem(){Id=2,Title="Solicitar Asesorias",TargetType = typeof(SolicitudPage),IconPath="ic_view_dashboard.png" },
            });
            SignOut = new Command(async () => await DeleteSession());

        }
        
        async Task DeleteSession()
        {
            var askClose = await Application.Current.MainPage.DisplayAlert(
                "Cerrar Sesion",
                "¿Estas Seguro que deseas cerrar sesion?",
                "Cancelar","Ok" 
            );

            if (askClose)
                return;

            NavigateToCurrentPage(new LoginPage());
        }

    }

    

}
