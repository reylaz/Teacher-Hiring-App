using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
namespace TeacherHiring.DataBase.Access
{
    class MateriaDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<DataBase.Model.Materia> Materia { get; set; }

        public MateriaDataAccess()
        {
            database = DependencyService.Get<ISQLite>().DbConnection();
            database.CreateTable<DataBase.Model.Materia>();
            this.Materia = new ObservableCollection<DataBase.Model.Materia>(database.Table<DataBase.Model.Materia>());

        }

        public DataBase.Model.Materia GetMateriaById(int id)
        {
            lock (collisionLock)
            {
                return database.Table<DataBase.Model.Materia>().FirstOrDefault(Materia => Materia.MateriaId == id);
            }
        }

        public int SaveMateria(DataBase.Model.Materia materia)
        {
            lock (collisionLock)
            {
                database.Insert(materia);
                return materia.MateriaId;
            }
        }

        public int UpdateMateria (DataBase.Model.Materia materia)
        {
            lock (collisionLock)
            {
                database.Update(materia);
                return materia.MateriaId;
            }
        }
        public void DeleteAllMateria()
        {
            lock (collisionLock)
            {
                database.DropTable<DataBase.Model.Materia>();
                database.CreateTable<DataBase.Model.Materia>();
            }
            this.Materia = null;
            this.Materia = new ObservableCollection<DataBase.Model.Materia>
              (database.Table<DataBase.Model.Materia>());
        }
    }
}
