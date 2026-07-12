namespace APIperson.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
        public Person() { }
        public Person(int id, string name, int age, string cpf)
        {
            ID = id;
            Nome = name;
            Idade = age;
            CPF = cpf;
        }
    }
}
