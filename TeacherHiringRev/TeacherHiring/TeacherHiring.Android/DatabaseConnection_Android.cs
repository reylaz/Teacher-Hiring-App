using SQLite;
using TeacherHiring.Droid;
using System.IO;
using TeacherHiring;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace TeacherHiring.Droid
{
  public class DatabaseConnection_Android : ISQLite
  {
    public SQLiteConnection DbConnection()
    {
      var dbName = "TeacherAppDB.db3";
      var path = Path.Combine(System.Environment.
        GetFolderPath(System.Environment.
        SpecialFolder.Personal), dbName);
      return new SQLiteConnection(path);
    }
  }
}
