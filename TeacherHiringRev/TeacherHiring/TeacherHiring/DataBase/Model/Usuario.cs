using System;
using SQLite;
using System.ComponentModel;

namespace TeacherHiring.DataBase.Model
{
    [Table("Usuario")]
    public class Usuario : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey]
        public int Id
        {
            get { return _id; }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _nombre;
        [NotNull]
        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                this._nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        private string _claveAcceso;
        [NotNull]
        public string ClaveAcceso
        {
            get
            {
                return _claveAcceso;
            }
            set
            {
                this._claveAcceso = value;
                OnPropertyChanged(nameof(ClaveAcceso));
            }
        }

        private string _contrasena;
        [NotNull]
        public string Contrasena
        {
            get
            {
                return _contrasena;
            }
            set
            {
                this._contrasena = value;
                OnPropertyChanged(nameof(Contrasena));
            }
        }

        private bool _profesor;
        [NotNull]
        public bool Profesor
        {
            get
            {
                return _profesor;
            }
            set
            {
                this._profesor = value;
                OnPropertyChanged(nameof(Profesor));
            }
        }

        private int _idTipoPerson;
        [NotNull]
        public int IdTipoPerson
        {
            get
            {
                return _idTipoPerson;
            }
            set
            {
                this._idTipoPerson = value;
                OnPropertyChanged(nameof(IdTipoPerson));
            }
        }

        private DateTime _clientCreatedOn;
        public DateTime ClientCreatedOn
        {
            get
            {
                return _clientCreatedOn;
            }
            set
            {
                this._clientCreatedOn = value;
                OnPropertyChanged(nameof(ClientCreatedOn));
            }
        }

        private string _clientId;
        public string ClientID
        {
            get
            {
                return _clientId;
            }
            set
            {
                this._clientId = value;
                OnPropertyChanged(nameof(ClientID));
            }
        }


        private string _clientSecret;
        public string ClientSecret
        {
            get
            {
                return _clientSecret;
            }
            set
            {
                this._clientSecret = value;
                OnPropertyChanged(nameof(ClientSecret));
            }
        }

        private string _token;
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                this._token = value;
                OnPropertyChanged(nameof(Token));
            }
        }

        private DateTime _tokenExpiry;
        public DateTime TokenExpiry
        {
            get
            {
                return _tokenExpiry;
            }
            set
            {
                this._tokenExpiry = value;
                OnPropertyChanged(nameof(TokenExpiry));
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
