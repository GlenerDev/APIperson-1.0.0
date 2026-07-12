using APIperson.Interfaces;
using APIperson.Models;
using APIperson.Models.DTOs;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;
using Dapper;


namespace Repository
{
    public class DBase : IDBaseRepository
    {
        public readonly string _ConnectionString;
        public DBase(string connectionstring) => _ConnectionString = connectionstring;
        public void OpenConnect(SQLiteConnection conn)
        {
            try
            {
                conn.Open();
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Disconnect(SQLiteConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool PersonExist(int id = 0)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                string comand = $"SELECT EXISTS (SELECT 1 FROM Pessoas WHERE ID = @id);";
                var resultboolean = conn.ExecuteScalar<bool>(comand, new { id });
                return resultboolean;
            }
        }
        public List<Person> GetAllUsers()
        {
            List<Person> resultlist = new List<Person>();
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                var cmd = "SELECT * FROM Pessoas ORDER BY ID DESC;";// tbm tem o ASC para crescente;
                resultlist = conn.Query<Person>(cmd).ToList();//retornar a lista de todos os usuarios do banco de de Dados
            }
            return resultlist;
        }
        public void CreatePerson(Person person)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                string sqlcmd = "INSERT INTO Pessoas (Nome, Idade, CPF) VALUES (@Nome, @Idade, @CPF)";//comando para inserir valores dentro das informaçoes de uma tabela
                conn.Execute(sqlcmd, new { Nome = person.Nome, Idade = person.Idade, CPF = person.CPF });
            }
        }
        public Person GetIDPerson(int id)
        {
            Person result = new Person();
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                string cmdString = "SELECT * FROM Pessoas WHERE ID = @ID;";
                result = conn.QueryFirstOrDefault<Person>(cmdString, new { ID = id })!;
            }
            return result;
        }
        public void UpdatePersonId(int id, string name, int age, string cpf)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                string stringcommand = "UPDATE Pessoas SET Nome = @name, Idade = @age, CPF = @cpf WHERE ID = @id;";
                conn.Execute(stringcommand, new { name, age, cpf, id });
                
            }
        }
        public void DeleteForId(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                string strcommand = "DELETE FROM Pessoas WHERE ID = @id;";
                conn.Execute(strcommand, new { id });
               
            }
        }
    }
}
