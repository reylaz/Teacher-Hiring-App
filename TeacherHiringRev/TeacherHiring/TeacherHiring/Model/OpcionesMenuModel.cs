using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherHiring.Views;

namespace TeacherHiring.Model
{
    public class OpcionesMenuModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string viewName { get; set; }
        public Type TargetType { get; set; }

        public OpcionesMenuModel(int _id, string _text, string _viewName)
        {
            this.Id = _id;
            this.Title = _text;
            this.viewName = _viewName;
            this.TargetType = typeof(MaestroAltaCurso);
        }
    }
    class OpcionesMenuMetodo
    {
        public static List<OpcionesMenuModel> getOpcionesMenu(int tipoUsr)
        {
            List<OpcionesMenuModel> opciones = new List<OpcionesMenuModel>();

            if (tipoUsr == 1)
            {
                opciones.Add(new OpcionesMenuModel(1, "Dar Asesoría", "MaestroAltaCurso"));
                opciones.Add(new OpcionesMenuModel(2, "Tus Asesorías", "MaestroAltaCurso"));
            }
            else
            {
                opciones.Add(new OpcionesMenuModel(1, "Buscar Asesoría", "MaestroAltaCurso"));
                opciones.Add(new OpcionesMenuModel(2, "Tus Asesorías", "MaestroAltaCurso"));
            }
            return opciones;
        }
    }
}
