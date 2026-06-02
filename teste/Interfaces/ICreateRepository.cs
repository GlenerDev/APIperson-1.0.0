namespace APIperson.Interfaces
{
    public interface ICreateRepository
    {
        public void CreatePerson(string name, int age, string CPF);
        public void CreateTable(string nametable, params string[] colums);
    }
}
