using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace TeacherHiring.DataBase.Access
{
    class ProfesorMateriaDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<DataBase.Model.ProfesorMateria> ProfesorMateria { get; set; }
        // Data operation here ...

        public ProfesorMateriaDataAccess()
        {
            database = DependencyService.Get<ISQLite>().DbConnection();
            database.CreateTable<DataBase.Model.ProfesorMateria>();
            this.ProfesorMateria = new ObservableCollection<DataBase.Model.ProfesorMateria>(database.Table<DataBase.Model.ProfesorMateria>());
            // If the table is empty, initialize the collection
            /*if (!database.Table<DataBase.Model.Usuario>().Any())
            {
                AddNewCustomer();
            }*/
        }

        public void AddProfesorMateria(DataBase.Model.ProfesorMateria _profesorMateria)
        {
            this.ProfesorMateria.Add(new DataBase.Model.ProfesorMateria
            {
                IdProfesorMateria = _profesorMateria.IdProfesorMateria,
                IdProfesor = _profesorMateria.IdProfesor,
                IdMateria = _profesorMateria.IdMateria,
                NombreMateria = _profesorMateria.NombreMateria,
                NombreProfesor = _profesorMateria.NombreProfesor,
                FechaHora = _profesorMateria.FechaHora,
                Latitud = _profesorMateria.Latitud,
                Longitud = _profesorMateria.Longitud
            });
        }

        public IEnumerable<DataBase.Model.ProfesorMateria> GetProfesorMateriaByIdProfesor(int _idProfesor)
        {
            lock (collisionLock)
            {
                var query = from ProfesorMateria in database.Table<DataBase.Model.ProfesorMateria>()
                            where ProfesorMateria.IdProfesor == _idProfesor
                            select ProfesorMateria;
                return query.AsEnumerable();
            }
        }

        public DataBase.Model.ProfesorMateria GetProfesorMateriaByProfesoMateriaId(int _idProfesorMateria)
        {
            lock (collisionLock)
            {
                return database.Table<DataBase.Model.ProfesorMateria>().FirstOrDefault(ProfesorMateria => ProfesorMateria.IdProfesorMateria == _idProfesorMateria);
            }
        }

        public int SaveProfesorMateria(DataBase.Model.ProfesorMateria _profesorMateria)
        {
            lock (collisionLock)
            {
                database.Insert(_profesorMateria);
                return _profesorMateria.IdProfesorMateria;    
            }
        }

        public int UpdateProfesorMateria(DataBase.Model.ProfesorMateria _profesorMateria)
        {
            lock (collisionLock)
            {
                database.Update(_profesorMateria);
                return _profesorMateria.IdProfesorMateria;
            }
        }

        public void DeleteAllProfesorMateria()
        {
            lock (collisionLock)
            {
                database.DropTable<DataBase.Model.ProfesorMateria>();
                database.CreateTable<DataBase.Model.ProfesorMateria>();
            }
            this.ProfesorMateria = null;
            this.ProfesorMateria = new ObservableCollection<DataBase.Model.ProfesorMateria>
              (database.Table<DataBase.Model.ProfesorMateria>());
        }


    }
}