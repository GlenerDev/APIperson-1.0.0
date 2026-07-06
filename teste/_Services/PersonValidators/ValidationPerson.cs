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
            bool result = false;
            if (Db.PersonExist(person.Id)) 
            {
                throw new ArgumentException("essa pessoa ja existe, coloque campos diferentes ou atualize na função a-baixo.");
            }
            if (person == null || string.IsNullOrEmpty(person.Name) || person.Age < 0 || string.IsNullOrEmpty(person.CPF))
            {
                result = false;
            }
            else { result = true; }
            return result;
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
