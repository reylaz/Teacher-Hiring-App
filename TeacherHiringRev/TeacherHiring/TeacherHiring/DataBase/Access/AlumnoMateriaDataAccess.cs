using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace TeacherHiring.DataBase.Access
{
    class AlumnoMateriaDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<DataBase.Model.AlumnoMateria> AlumnoMateria { get; set; }
        // Data operation here ...

        public AlumnoMateriaDataAccess()
        {
            database = DependencyService.Get<ISQLite>().DbConnection();
            database.CreateTable<DataBase.Model.AlumnoMateria>();
            this.AlumnoMateria = new ObservableCollection<DataBase.Model.AlumnoMateria>(database.Table<DataBase.Model.AlumnoMateria>());
            // If the table is empty, initialize the collection
            /*if (!database.Table<DataBase.Model.Usuario>().Any())
            {
                AddNewCustomer();
            }*/
        }

        public void AddAlumnoMateria(DataBase.Model.AlumnoMateria _alumnoMateria)
        {
            this.AlumnoMateria.Add(new DataBase.Model.AlumnoMateria
            {
                IdAlumnoMateria = _alumnoMateria.IdAlumnoMateria,
                IdProfesorMateria = _alumnoMateria.IdProfesorMateria,
                IdAlumno = _alumnoMateria.IdAlumno,
                IdProfesor = _alumnoMateria.IdProfesor,
                IdMateria = _alumnoMateria.IdMateria,
                NombreAlumno = _alumnoMateria.NombreAlumno,
                NombreMateria = _alumnoMateria.NombreMateria,
                NombreProfesor = _alumnoMateria.NombreProfesor,
                FechaHora = _alumnoMateria.FechaHora,
                Latitud = _alumnoMateria.Latitud,
                Longitud = _alumnoMateria.Longitud,
                Aceptada = _alumnoMateria.Aceptada
            });
        }

        public IEnumerable<DataBase.Model.AlumnoMateria> GetAlumnoMateriaByIdAlumno(int _idAlumno)
        {
            lock (collisionLock)
            {
                var query = from AlumnoMateria in database.Table<DataBase.Model.AlumnoMateria>()
                            where AlumnoMateria.IdAlumno == _idAlumno
                            select AlumnoMateria;
                return query.AsEnumerable();
            }
        }

        public DataBase.Model.AlumnoMateria GetAlumnoMateriaByIdAlumnoMateria(int _idAlumnoMateria)
        {
            lock (collisionLock)
            {
                return database.Table<DataBase.Model.AlumnoMateria>().FirstOrDefault(AlumnoMateria => AlumnoMateria.IdAlumnoMateria == _idAlumnoMateria);
            }
        }

        public int SaveAlumnoMateria(DataBase.Model.AlumnoMateria _alumnoMateria)
        {
            lock (collisionLock)
            {
                if (_alumnoMateria.IdAlumnoMateria != 0)
                {
                    database.Update(_alumnoMateria);
                    return _alumnoMateria.IdAlumnoMateria;
                }
                else
                {
                    database.Insert(_alumnoMateria);
                    return _alumnoMateria.IdAlumnoMateria;
                }
            }
        }

        public void DeleteAllAlumnoMateria()
        {
            lock (collisionLock)
            {
                database.DropTable<DataBase.Model.AlumnoMateria>();
                database.CreateTable<DataBase.Model.AlumnoMateria>();
            }
            this.AlumnoMateria = null;
            this.AlumnoMateria = new ObservableCollection<DataBase.Model.AlumnoMateria>
              (database.Table<DataBase.Model.AlumnoMateria>());
        }
        
    }
}