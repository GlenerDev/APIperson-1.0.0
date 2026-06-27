using APIperson._Services.PersonValidations;
using APIperson.Interfaces;
using Repository;
using System.Data;
using System.Data.SQLite;

namespace APIperson._Services
{
    public class RepositoryServices : DBase, IDBaseRepository
    {
        public readonly DBase DB;
        public RepositoryServices(DBase dBase)
        {
            DB = dBase;
        }
        public bool VerifyOpenConnect()
        {
            if (_Connection.State == ConnectionState.Open)
            {
                return true;
            }
            return false;
        }
        public void OpenConnect()
        {
            if (_Connection.State == ConnectionState.Closed)
            {
                DB.OpenConnect();
            }
            throw new Exception("O Banco já esta aberto");
        }
        public void Disconnect()
        {
            if (_Connection.State == ConnectionState.Open)
            {
                DB.OpenConnect();
            }
            throw new Exception("O Banco já esta Fechado");
        }
    }
}
