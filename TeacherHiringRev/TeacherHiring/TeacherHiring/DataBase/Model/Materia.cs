using System;
using SQLite;
using System.ComponentModel;
namespace TeacherHiring.DataBase.Model
{
    [Table("Materia")]
    public class Materia : INotifyPropertyChanged
    {
        private int _materiaId;
        [PrimaryKey]
        public int MateriaId
        {
            get
            {
                return _materiaId;
            }

            set
            {
                this._materiaId = value;
                OnPropertyChanged(nameof(MateriaId));

            }
        }

        private string _descripcion;
        public string Descripcion
        {
            get
            {
                return _descripcion;
            }

            set
            {
                this._descripcion = value;
                OnPropertyChanged(nameof(Descripcion));
            }
        }

        private int _disponibles;
        public int Disponibles
        {
            get
            {
                return _disponibles;
            }
            set
            {
                this._disponibles = value;
                OnPropertyChanged(nameof(Disponibles));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
