using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SQLite;
using teste.Models;

namespace teste.Dbase
{
    public  class DBase
    {
        public SQLiteConnection sqlite = new SQLiteConnection(@"Data Source=C:\Projetos\Bancos\DbPerson.db");
        public DBase()
        {
            Connect();
        }
        public  bool Connect()
        {
            try
            {
                sqlite.Open();
                return (sqlite.State == ConnectionState.Open);
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public  bool Disconnect()
        {
            try
            {
                if (sqlite.State != ConnectionState.Open) return false;
                sqlite.Close();
                return true;
            }
            catch (SQLiteException ex)
            {
                new Exception($"{ex.Message}[DBase.Connect]");
                return false;
            }
        }
        public  void CreatedPersonForDataBase(Person person) 
        {
            if (Connect()) 
            {
                string sqlcmd = "INSERT INTO Pessoas (ID, Name, Idade, CPF) VALUES (@ID, @Name, @Idade, @CPF)";//comando para inserir valores dentro das informaçoes de uma tabela

                using (var cmd = new SQLiteCommand(sqlcmd, sqlite)) 
                {
                    cmd.Parameters.AddWithValue("@ID",person.ID);
                    cmd.Parameters.AddWithValue("@Name",person.name);
                    cmd.Parameters.AddWithValue("@Idade",person.idade);
                    cmd.Parameters.AddWithValue("@CPF", person.cpf);
                    cmd.ExecuteNonQuery();
                }

            }
        }
        public void CreateTableForDataBase(string nametable,string nome,string Idade, string CPF, string Id = "") 
        {
           
            string cmdSQL = 
                "CREATE TABLE " +
                "@nametable " +
                "(" +
                "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                "@nome TEXT NOT NULL," +
                "@idade INTEGER NOT NULL," +
                "@CPF TEXT NOT NULL);";

            var cmdcreatetable = new SQLiteCommand(cmdSQL, sqlite);
            cmdcreatetable.Parameters.AddWithValue("@Id", Id);
            cmdcreatetable.Parameters.AddWithValue("@nome", nome);
            cmdcreatetable.Parameters.AddWithValue("@idade", Idade);
            cmdcreatetable.Parameters.AddWithValue("@CPF", CPF);
            cmdcreatetable.ExecuteNonQuery();
        }
        public List<Person> GetAllUsers()
        {
            List<Person> resultlist = new List<Person>();
            var cmd = "SELECT * FROM Pessoas";
            var sqlitecomand = new SQLiteCommand(cmd,sqlite);// criar um objeto SQLiteCommand com o comando e o banco de dados 
            SQLiteDataReader reader = sqlitecomand.ExecuteReader();// leitor SQLite dos dados presentes no banco de dados
            while (reader.Read())// Enquanto estiver intens para serem lidos, o item será adicionado dentro da lista de persons 
            {
                Person p = new Person(Convert.ToInt32(reader["Idade"]), reader["Name"].ToString()!,"111.223.455-60"); // cria um nova instancia do item lido.
                                                                                                          
                resultlist.Add(p);//adiciona o objeto dentro da lista
            }
            Results.Ok("leitura finalizada");//indica que todos os itens do bancos de dados já foram lidos.
            return resultlist;//retornar a lista de todos os usuarios do banco de de Dados

        }
    }
}
