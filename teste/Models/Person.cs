namespace teste.Models
{
    public class Person
    {
        public int? ID { get; set; }
        public string name { get; private set; } 
        public int idade { get; private set; } 
        public string cpf { get; private set; } 

        public Person(int idade = 0, string name = "000.000.000-00")
        {
            this.name = name;
            this.idade = idade;
        }
    }
}
