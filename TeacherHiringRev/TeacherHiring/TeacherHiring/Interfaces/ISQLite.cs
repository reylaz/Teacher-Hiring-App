using SQLite;

namespace TeacherHiring
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
