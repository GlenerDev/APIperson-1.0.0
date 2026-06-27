namespace APIperson.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string CPF { get; set; }
        public Person() { }
        public Person(int id, string name, int age, string cpf)
        {
            Id = id;
            Name = name;
            Age = age;
            CPF = cpf;
        }
    }
}
