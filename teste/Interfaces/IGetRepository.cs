using APIperson.Models;
using APIperson.Models.DTOs;

namespace APIperson.Interfaces
{
    internal interface IGetRepository
    {
        public Person GetIDPerson(int id);
        public List<Person> GelAllUsers();
    }
}