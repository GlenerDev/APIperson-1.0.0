using APIperson._Services.PersonValidations;
using APIperson.Interfaces;
using Repository;
using System.Data;
using System.Data.SQLite;

namespace APIperson._Services
{
    public class RepositoryServices : IDBaseRepository
    {
        public readonly DBase DB;
        public RepositoryServices(DBase dBase) => DB = dBase;
        public bool VerifyOpenConnect()
        {
            var coon = new SQLiteConnection(DB._ConnectionString);
            try
            {
                coon.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally 
            {
                coon.Close();
            }
        }
        public void OpenConnect(SQLiteConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                DB.OpenConnect(conn);
            }
            throw new Exception("O Banco já esta aberto");
        }
        public void Disconnect(SQLiteConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                DB.OpenConnect(conn);
            }
            throw new Exception("O Banco já esta Fechado");
        }
    }
}
