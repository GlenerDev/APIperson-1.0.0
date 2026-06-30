using APIperson.Models;
using Repository;
using System.Data.Common;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace APIperson._Services.PersonValidations
{
    public class ValidationPerson
    {
        private DBase Db { get; set; }
        public ValidationPerson(DBase db)
        {
            Db = db;
        }
        
        public bool ValidationCreatePerson(Person person)
        {
            if (!Db.PersonExist(person.Id) ||
                person == null ||
                string.IsNullOrEmpty(person.Name) ||
                person.Age < 0 ||
                string.IsNullOrEmpty(person.CPF))
            {
                return false;
            }
            return true;
        }
        public bool ValidationCPF(string cpf)
        {
            string parternregex = @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$";
            return Regex.IsMatch(cpf, parternregex);
        }
        public bool IdValidation(int id)
        {
            if (id < 0)
            {
                return false;
            }
            return true;
        }
    }
}
