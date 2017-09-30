using Acr.UserDialogs;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherHiring.DataBase.Access;
using TeacherHiring.Views;
using Xamarin.Forms;


namespace TeacherHiring
{
	class LoginViewModel:BaseViewModel
    {
        MateriaDataAccess materiaDataAccess = new MateriaDataAccess();
        UsuarioDataAccess usuarioDataAccess = new UsuarioDataAccess();
        DataBase.Model.Usuario _user;
        Boolean tipoPerson;
        public DataBase.Model.Usuario User {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(); }
        }

        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            tipoPerson = true;
            LoginCommand = new Command(async () => await Login());
            User = new DataBase.Model.Usuario();

        }

        async Task Login()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                usuarioDataAccess.DeleteAllUsuario();
                User.ClientCreatedOn = DateTime.Now;
                User.ClientID = string.Empty;
                User.ClientSecret = string.Empty;
                User.Id = 0;
                User.IdTipoPerson = Convert.ToInt32(tipoPerson);
                User.Nombre = string.Empty;
                User.ClaveAcceso = _user.ClaveAcceso;
                User.Contrasena = _user.Contrasena;

               
                using (UserDialogs.Instance.Loading("Verificando.."))
                {
                    User = await ServiceAPI.Authenticate(User);
                }
                if (string.IsNullOrEmpty(User.Token))
                {
                    UserDialogs.Instance.ShowError("Acceso denegado");
                    return;
                }
                else
                {
                    userGlobal.ClaveAcceso = User.ClaveAcceso;
                    userGlobal.IdTipoPerson = User.IdTipoPerson;
                    userGlobal.Contrasena = User.Contrasena;
                    userGlobal.Token = User.Token;

                    materiaDataAccess.DeleteAllMateria();
                    materias = await ServiceAPI.GetListMaterias(userGlobal.Token);

                    NavigateToCurrentPage (new TeacherHiring.Views.Menu());
                }
                
                
                /*var deviceInfo = CrossDeviceInfo.Current;
                await Application.Current.MainPage.DisplayAlert("Id", deviceInfo.Id, "Ok");
                await Application.Current.MainPage.DisplayAlert("Language", deviceInfo.Idiom.ToString(), "Ok");
                await Application.Current.MainPage.DisplayAlert("Model", deviceInfo.Model, "Ok");
                await Application.Current.MainPage.DisplayAlert("Platform", deviceInfo.Platform.ToString(), "Ok");
                await Application.Current.MainPage.DisplayAlert("Version", deviceInfo.Version, "Ok");
                await Application.Current.MainPage.DisplayAlert("VersionNumber", deviceInfo.VersionNumber.Revision.ToString(), "Ok");
                */
            }
	    	catch { }
		    finally
		    {
		        IsBusy = false;
		    }
		}
    }
}