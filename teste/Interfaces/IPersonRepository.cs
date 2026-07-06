using APIperson.Models;
using APIperson.Models.DTOs;

namespace APIperson.Interfaces
{
    public interface IPersonRepositoryService
    {
        public void ServiceCreatePerson(Person person);
        public Person ServiceGetIDPerson(int id);
        public List<DTOPerson> ServiceGetAllUsers();
        public Task ServiceDeleteForId(int id);
        public Task ServiceUpdatePersonId(int id, string nome,int idade,string cpf);
    }
}
