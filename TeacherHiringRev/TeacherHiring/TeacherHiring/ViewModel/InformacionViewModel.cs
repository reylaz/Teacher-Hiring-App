using Acr.UserDialogs;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherHiring.Model;
using TeacherHiring.Views;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    class InformacionViewModel:BaseViewModel
    {
        private Informacion _informacion { get; set; }
        public string FullName
        {
            get { return _informacion.FullName; }
            set { _informacion.FullName = value; RaisePropertyChanged(); }
        }

        public string UserName
        {
            get { return _informacion.UserName; }
            set { _informacion.UserName = value; RaisePropertyChanged(); }
        }

        public string Password
        {
            get { return _informacion.Password; }
            set { _informacion.Password = value; RaisePropertyChanged(); }
        }

        public string LatLong
        {
            get { return _informacion.LatLong; }
            set { _informacion.LatLong = value; RaisePropertyChanged(); }
        }


        public IGeolocator GeoLocator { get; set; }
        public ICommand GetLocation { get; set; }
        public ICommand SaveInformation { get; set; }

        public InformacionViewModel()
        {
            _informacion = new Informacion();
            GeoLocator = CrossGeolocator.Current;
            GetLocation = new Command(async () => await GetUserLocationAsync());
            SaveInformation = new Command(async () => await SaveInformationAsync());
        }

        public async Task GetUserLocationAsync()
        {
            if (IsBusy)
                return;

            if(!GeoLocator.IsGeolocationEnabled)
            {
                await Application.Current.MainPage.DisplayAlert("Error Localizacion", "Es necesario activar el GPS", "Ok");
                IsBusy = false;
                return;
            }

            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo Ubicacion...");
                Position position = await GeoLocator.GetPositionAsync((int)TimeSpan.FromSeconds(8).TotalMilliseconds);
                UserDialogs.Instance.HideLoading();
                if(position==null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error Localizacion", "No se pudo obtener tu ubicacion", "Ok");
                    IsBusy = false;
                    return;
                }
                LatLong = position.Latitude.ToString() + "," + position.Longitude.ToString();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task SaveInformationAsync()
        {
            if (IsBusy)
                return;


            try
            {
                await GetUserLocationAsync();
                using (UserDialogs.Instance.Loading("Guardando Informacion..."))
                {
                    await Task.Delay(3000);
                }

                //NavigateToCurrentPage(new ShellPage());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
