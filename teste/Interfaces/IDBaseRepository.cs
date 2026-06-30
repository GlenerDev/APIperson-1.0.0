using System.Data.SQLite;

namespace APIperson.Interfaces
{
    public interface IDBaseRepository
    {
        public void OpenConnect(SQLiteConnection conn);
        public  void Disconnect(SQLiteConnection conn);
    }
}
