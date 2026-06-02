using System.Text.RegularExpressions;

namespace APIperson.Models.DTOs
{
    public class DTOPerson
    {
        public string Name { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
        public DTOPerson()
        {
        }
    }
}
