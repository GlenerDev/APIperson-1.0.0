using APIperson.Models;
using Repository;
using System.Data.Common;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace APIperson._Services.PersonValidations
{
    public class ValidationPerson : DBase
    {
        public ValidationPerson(SQLiteConnection conection) : base(conection) { }
        protected bool PersonExist(int id)
        {
            OpenConnect();
            string comand = $"SELECT 1 WHERE EXISTS (SELECT 1 FROM Pessoas WHERE ID = '{id}');";
            SQLiteCommand sqlcomand = new SQLiteCommand(comand, _Connection);
            SQLiteDataReader reader = sqlcomand.ExecuteReader();
            bool resultboolean = reader.Read();
            Disconnect();
            return resultboolean;
        }
        protected bool ValidationCreatePerson(string name, int age, string CPF)
        {
            if (string.IsNullOrEmpty(name) || age < 0 || string.IsNullOrEmpty(CPF)) return false;
            return true;
        }
        protected bool ValidationCPF(string cpf) 
        {
            string parternregex = @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$";
            return Regex.IsMatch(cpf, parternregex);
        }
    }
}
