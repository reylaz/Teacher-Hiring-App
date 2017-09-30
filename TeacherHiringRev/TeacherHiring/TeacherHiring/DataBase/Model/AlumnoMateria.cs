using System;
using SQLite;
using System.ComponentModel;

namespace TeacherHiring.DataBase.Model
{
    [Table("AlumnoMateria")]
    public class AlumnoMateria : INotifyPropertyChanged
    {
        private int _idAlumnoMateria;
        [PrimaryKey, AutoIncrement]
        public int IdAlumnoMateria
        {
            get
            {
                return _idAlumnoMateria;
            }

            set
            {
                this._idAlumnoMateria = value;
                OnPropertyChanged(nameof(IdAlumnoMateria));

            }
        }

        private int _idProfesorMateria;
        public int IdProfesorMateria
        {
            get
            {
                return _idProfesorMateria;
            }

            set
            {
                this._idProfesorMateria = value;
                OnPropertyChanged(nameof(IdProfesorMateria));
            }
        }

        private int _idAlumno;
        public int IdAlumno
        {
            get
            {
                return _idAlumno;
            }

            set
            {
                this._idAlumno = value;
                OnPropertyChanged(nameof(IdAlumno));

            }
        }

        private int _idProfesor;
        public int IdProfesor
        {
            get
            {
                return _idProfesor;
            }

            set
            {
                this._idProfesor = value;
                OnPropertyChanged(nameof(IdProfesor));

            }
        }

        private int _idMateria;
        public int IdMateria
        {
            get
            {
                return _idMateria;
            }

            set
            {
                this._idMateria = value;
                OnPropertyChanged(nameof(IdMateria));

            }
        }

        private string _nombreAlumno;
        public string NombreAlumno
        {
            get
            {
                return _nombreAlumno;
            }

            set
            {
                this._nombreAlumno = value;
                OnPropertyChanged(nameof(NombreAlumno));

            }
        }

        private string _nombreMateria;
        public string NombreMateria
        {
            get
            {
                return _nombreMateria;
            }

            set
            {
                this._nombreMateria = value;
                OnPropertyChanged(nameof(NombreMateria));

            }
        }

        private string _nombreProfesor;
        public string NombreProfesor
        {
            get
            {
                return _nombreProfesor;
            }

            set
            {
                this._nombreProfesor = value;
                OnPropertyChanged(nameof(NombreProfesor));

            }
        }

        private DateTime _fechaHora;
        public DateTime FechaHora
        {
            get
            {
                return _fechaHora;
            }

            set
            {
                this._fechaHora = value;
                OnPropertyChanged(nameof(FechaHora));

            }
        }

        private decimal _latitud;
        public decimal Latitud
        {
            get
            {
                return _latitud;
            }

            set
            {
                this._latitud = value;
                OnPropertyChanged(nameof(Latitud));

            }
        }

        private decimal _longitud;
        public decimal Longitud
        {
            get
            {
                return _longitud;
            }

            set
            {
                this._longitud = value;
                OnPropertyChanged(nameof(Longitud));

            }
        }

        private Boolean _aceptada;
        public Boolean Aceptada
        {
            get
            {
                return _aceptada;
            }

            set
            {
                this._aceptada = value;
                OnPropertyChanged(nameof(Aceptada));

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
