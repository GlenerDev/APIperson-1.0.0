using APIperson._Services.PersonValidations;
using APIperson.Interfaces;
using Repository;
using System.Data.SQLite;

namespace APIperson._Services
{
    public class RepositoryServices : DBase , IDBaseRepository
    {
        public readonly DBase DB;
        private ValidationPerson _validations;
        public RepositoryServices(DBase dBase,ValidationPerson validation)
        {
            DB = dBase;
            _validations = validation;
            
        }
        public bool VerifyOpenConnect() 
        {
            if(_Connection.State ==  )
        }
        
        public void OpenConnect() 
        {
            
        }
        public void Disconnect() 
        {
        
        }
    }
}
