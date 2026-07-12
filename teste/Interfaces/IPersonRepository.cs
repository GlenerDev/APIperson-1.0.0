using APIperson.Models;
using APIperson.Models.DTOs;

namespace APIperson.Interfaces
{
    public interface IPersonRepositoryService
    {
        public Task<Person> ServiceGetIDPerson(int id);
        public Task<List<Person>> ServiceGetAllUsers();
        public Task ServiceCreatePerson(Person person);
        public Task ServiceDeleteForId(int id);
        public Task ServiceUpdatePersonId(int id, string nome,int idade,string cpf);
    }
}
