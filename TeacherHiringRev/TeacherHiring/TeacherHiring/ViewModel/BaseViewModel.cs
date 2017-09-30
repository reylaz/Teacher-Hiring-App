using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeacherHiring;
using Xamarin.Forms;
using TeacherHiring.DataBase.Model;

namespace TeacherHiring
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public TeacherAPI ServiceAPI => new TeacherAPI();
        public static INavigation Navigation { get; set; }
        public static MasterDetailPage MasterDetail { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Usuario _userGlobal = new Usuario();
        public Usuario userGlobal
        {
            get
            {
                return _userGlobal;
            }
            set
            {
                _userGlobal = value;
            }
        }
        private List<DataBase.Model.Materia> _materias = new List<DataBase.Model.Materia>();
        public List<DataBase.Model.Materia> materias
        {
            get
            {
                return _materias;
            }
            set
            {
                _materias = value;
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public async Task NavigateTo(Page viewPage)
        {
            await Navigation.PushModalAsync(viewPage);
        }

        public async Task MasterNavigateTo(Page viewPage)
        {
            MasterDetail.IsPresented = false;
            await MasterDetail.Detail.Navigation.PushAsync(viewPage);
        }

        public void NavigateToCurrentPage(Page viewPage)
        {
            Application.Current.MainPage = viewPage;
        }

        public void NavigateToBackPage()
        {
            Navigation.PopAsync();
        }
    }
}
