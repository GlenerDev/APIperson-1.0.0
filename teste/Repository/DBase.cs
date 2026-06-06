using APIperson.Interfaces;
using APIperson.Models;
using APIperson.Models.DTOs;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SQLite;
using System.Text;


namespace Repository
{
    public class DBase : ICreateRepository, IGetRepository, IVerifyRepository, IDBase
    {
        private readonly SQLiteConnection _Connection;
        public DBase(SQLiteConnection connectionsql)
        {
            _Connection = connectionsql;
        }
        
        public void Connect()
        {
            try
            {
                if (_Connection.State != ConnectionState.Open)
                    _Connection.Open();
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Disconnect()
        {
            try
            {
                if (_Connection.State != ConnectionState.Open)
                    _Connection.Close();
            }
            catch (SQLiteException ex)
            {
                new Exception(ex.Message);
            }
        }
        // / / / / / / function s verifiers // / / / / / / / / 
        public bool VerifyOpenConnect()
        {
            try
            {
                return _Connection.State == ConnectionState.Open;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel verificar a conexão");
            }
        }
        public bool VerifyCloseConnect()
        {
            try
            {
                return _Connection.State == ConnectionState.Open;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel verificar a conexão");
            }
        }
        // / / / / / /function create / / / / / / / / 
        public void CreatePerson(string name, int age, string CPF)
        {
            using (_Connection)
            {
                _Connection.Open();
                string sqlcmd = "INSERT INTO Pessoas (Nome, Idade, CPF) VALUES (@Name, @Idade, @CPF)";//comando para inserir valores dentro das informaçoes de uma tabela
                var cmd = new SQLiteCommand(sqlcmd, _Connection);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Idade", age);
                cmd.Parameters.AddWithValue("@CPF", CPF);
                cmd.ExecuteNonQuery();
            }
        }
        public void CreateTable(string nametable, params string[] colums)
        {
            using (_Connection)
            {
                _Connection.Open();
                var strbuild = new StringBuilder($"CREATE TABLE IF NOT EXISTS '{nametable}' (ID INTEGER PRIMARY KEY AUTOINCREMENT,");
                for (int i = 0; i < colums.Length; i++)
                {
                    strbuild.Append($"'{colums[i]}'");
                    if (i < colums.Length - 1) strbuild.Append(", ");
                }
                strbuild.Append(");");
                string cmdSQL = strbuild.ToString();
                var cmdcreatetable = new SQLiteCommand(cmdSQL, _Connection);
                cmdcreatetable.ExecuteNonQuery();
            }
        }
        // / / / / / function getters/ / / / / / / / 
        public List<Person> GelAllUsers()
        {
            List<Person> resultlist = new List<Person>();
            var cmd = "SELECT * FROM Pessoas";
            using (_Connection)
            {
                _Connection.Open();

                var sqlitecomand = new SQLiteCommand(cmd, _Connection);
                SQLiteDataReader reader = sqlitecomand.ExecuteReader();// leitor SQLite dos dados presentes no banco de dados
                while (reader.Read())// Enquanto estiver intens para serem lidos, o item será adicionado dentro da lista de persons 
                {
                    Person p = new Person(Convert.ToInt32(reader["ID"]),
                        Convert.ToInt32(reader["Idade"]),
                        reader["Nome"].ToString()!, reader["CPF"].ToString()); // cria um nova instancia do item lido.
                    resultlist.Add(p);//adiciona o objeto dentro da lista
                }
                return resultlist;//retornar a lista de todos os usuarios do banco de de Dados
            }
        }
        public Person GetIDPerson(int id)
        {
            string cmdString = $"SELECT * FROM Pessoas WHERE ID = '{id}';";

            using (_Connection)
            {
                _Connection.Open();
                SQLiteCommand sqliteComand = new SQLiteCommand(cmdString, _Connection);
                var reader = sqliteComand.ExecuteReader();
                var p = new Person();
                if (reader.Read())
                {
                    p.ID = Convert.ToInt32(reader["ID"]);
                    p.name = reader["Nome"].ToString()!;
                    p.idade = Convert.ToInt32(reader["Idade"]);
                    p.cpf = reader["CPF"].ToString()!;
                }
                return p;
            }
        }
        /////////////function delete///////////////
        public void DeleteForId(int id)
        {
            string strcommand = $"DELETE FROM Pessoas WHERE ID = '{id}'; ";
            using (_Connection) 
            {
                _Connection.Open();
                var cmd = new SQLiteCommand(strcommand, _Connection);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

