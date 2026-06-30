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


namespace Repository
{
    public class DBase : IDBaseRepository
    {
        public readonly string _ConnectionString;
        public DBase(string connectionstring) => _ConnectionString = connectionstring;
        public DBase() { }
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

        #region functions person
        public bool PersonExist(int id = 0)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                OpenConnect(conn);
                string comand = $"SELECT 1 WHERE EXISTS (SELECT 1 FROM Pessoas WHERE ID = @id);";
                SQLiteCommand sqlcomand = new SQLiteCommand(comand, conn);
                sqlcomand.Parameters.AddWithValue("@id", id);
                SQLiteDataReader reader = sqlcomand.ExecuteReader();
                bool resultboolean = reader.Read();
                return resultboolean;
            }
        }
        public List<DTOPerson> GetAllUsers()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                OpenConnect(conn);
                List<DTOPerson> resultlist = new List<DTOPerson>();
                var cmd = "SELECT * FROM Pessoas";
                var sqlitecomand = new SQLiteCommand(cmd, conn);
                SQLiteDataReader reader = sqlitecomand.ExecuteReader();// leitor SQLite dos dados presentes no banco de dados
                while (reader.Read())// Enquanto tiver intens para serem lidos, o item será adicionado dentro da lista de persons 
                {
                    // cria um nova instancia do item lido.
                    Person p = new Person(
                        Convert.ToInt32(
                        reader["ID"]),
                        reader["Nome"].ToString()!,
                        Convert.ToInt32(
                        reader["Idade"]),
                        reader["CPF"].ToString()!);

                    resultlist.Add(new DTOPerson(p.Id, p.Name, p.Age));//adiciona uma pessoa dentro da lista
                }
                return resultlist;//retornar a lista de todos os usuarios do banco de de Dados
            }
        }
        public void CreatePerson(Person person)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                OpenConnect(conn);
                string sqlcmd = "INSERT INTO Pessoas (Nome, Idade, CPF) VALUES (@Name, @Idade, @CPF)";//comando para inserir valores dentro das informaçoes de uma tabela
                var cmd = new SQLiteCommand(sqlcmd, conn);
                cmd.Parameters.AddWithValue("@Name", person.Name);
                cmd.Parameters.AddWithValue("@Idade", person.Age);
                cmd.Parameters.AddWithValue("@CPF", person.CPF);
                cmd.ExecuteNonQuery();
            }
        }
        public Person GetIDPerson(int id)
        {

            Person result = new Person();
            string cmdString = $"SELECT * FROM Pessoas WHERE ID = @id;";
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                OpenConnect(conn);
                SQLiteCommand sqliteComand = new SQLiteCommand(cmdString, conn);
                sqliteComand.Parameters.AddWithValue("@id", id);
                var reader = sqliteComand.ExecuteReader();
                var p = new Person();
                if (reader.Read())
                {
                    p.Id = Convert.ToInt32(reader["ID"]);
                    p.Name = reader["Nome"].ToString()!;
                    p.Age = Convert.ToInt32(reader["Idade"]);
                    p.CPF = reader["CPF"].ToString()!;
                }
                result = p;
            }
            return result;
        }
        public void UpdatePersonId(int id, string name, int age)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                OpenConnect(conn);
                string stringcommand = $"UPDATE Pessoas SET Nome = @name, Idade = @age, WHERE ID = @id;";
                SQLiteCommand cmdsql = new SQLiteCommand(stringcommand, conn);

                cmdsql.Parameters.AddWithValue("@name", name);
                cmdsql.Parameters.AddWithValue("@age", age);
                cmdsql.Parameters.AddWithValue("@id", id);
                cmdsql.ExecuteNonQuery();
            }
        }
        public void DeleteForId(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectionString))
            {
                OpenConnect(conn);
                string strcommand = $"DELETE FROM Pessoas WHERE ID = @id; ";
                var cmd = new SQLiteCommand(strcommand, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}

