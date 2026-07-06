namespace APIperson.Models.DTOs
{
    public class DTOCreatePerson
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
        public DTOCreatePerson(string nome, int idade, string cpf) 
        {
            Nome = nome;
            Idade = idade;
            CPF = cpf;
        }
    }
}
