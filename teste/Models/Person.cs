namespace APIperson.Models
{
    public class Person
    {
        public int? ID { get; set; }
        public string name { get;  set; } 
        public int idade { get;  set; } 
        public string cpf { get;  set; }
        public Person() { }
        public Person(int id,int idade, string nome, string CPF)
        {
            ID = id;
            name = nome;
            this.idade = idade;
            cpf = CPF;
        }
    }
}
