namespace teste.Models
{
    public class Person
    {
        public int? ID { get; set; }
        public string name { get;  set; } 
        public int idade { get;  set; } 
        public string cpf { get;  set; } 

        public Person(int idade, string nome, string CPF)
        {
            this.name = name;
            this.idade = idade;
            cpf = CPF;
        }
    }
}
