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
        public void CreateTable(string nametable, params object[] listvalues) 
        {
            
        }
    }
}
