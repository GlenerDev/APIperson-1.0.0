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
        public ValidationPerson() { }

        public bool PersonExist(int id = 0, string cpf = null)
        {
            Db.OpenConnect();
            string comand = $"SELECT 1 WHERE EXISTS (SELECT 1 FROM Pessoas WHERE ID = @id OR CPF = @cpf); ";
            SQLiteCommand sqlcomand = new SQLiteCommand(comand, Db._Connection);
            sqlcomand.Parameters.AddWithValue("@id", id);
            sqlcomand.Parameters.AddWithValue("@cpf", cpf);
            SQLiteDataReader reader = sqlcomand.ExecuteReader();
            bool resultboolean = reader.Read();
            Db.Disconnect();
            return resultboolean;
        }
        public bool ValidationCreatePerson(string name, int age, string CPF)
        {
            if (!PersonExist(0, CPF))
            {
                if (string.IsNullOrEmpty(name) || age < 0 || string.IsNullOrEmpty(CPF)) return false;
                return true;
            }
            return false;
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

            }
        }
    }
}
