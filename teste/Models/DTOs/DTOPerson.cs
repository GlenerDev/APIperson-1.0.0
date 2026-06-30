using System.Text.RegularExpressions;

namespace APIperson.Models.DTOs
{
    public class DTOPerson
    {
        public int? ID { get;  }
        public string Name { get;  }
        public int Idade { get; }
        public DTOPerson(int id, string name, int idade)
        {
            ID = id;
            Name = name;
            Idade = idade;
        }
        
    }
}
