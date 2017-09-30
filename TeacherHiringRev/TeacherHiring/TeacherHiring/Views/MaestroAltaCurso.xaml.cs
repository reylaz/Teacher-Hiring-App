using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaestroAltaCurso : ContentPage
    {

        public MaestroAltaCurso()
        {
            InitializeComponent();
            BindingContext = this;
            var map = new Map(
                MapSpan.FromCenterAndRadius(
                        new Position(0, 0), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                //HeightRequest = 300,
                //WidthRequest = 400,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            mapa.Children.Add(map);

        }
    }
}