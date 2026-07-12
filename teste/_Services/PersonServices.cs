using APIperson._Services.PersonValidations;
using APIperson.Interfaces;
using APIperson.Models;
using APIperson.Models.DTOs;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Repository;
using APIperson.Services;
using System.Data.SQLite;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace APIperson.Services
{
    public class PersonServices : IPersonRepositoryService
    {
        private readonly DBase DB;
        private readonly ValidationPerson Valid;
        public PersonServices(DBase dbase, ValidationPerson validationperson)
        {
            DB = dbase;
            Valid = validationperson;
        }
        public async Task ServiceCreatePerson(Person person)
        {
            if (Valid.ValidationCreatePerson(person))
            {
                DB.CreatePerson(person);
                return;
            }
            throw new ArgumentException("erro no corpo da requisição, por favor coloque as credencias corretas.");
        }
        public async Task<Person> ServiceGetIDPerson(int id)
        {
            
            if (Valid.IdValidation(id)) 
            {
                if (!DB.PersonExist(id))
                {
                    throw new NullReferenceException("Possoa não existe no banco de dados");
                }
                return DB.GetIDPerson(id);
            }
            throw new Exception($"{id} -error- number negative ");
        }
        public async Task<List<Person>> ServiceGetAllUsers()
        {
            if (DB.GetAllUsers().Count != 0)
            {
                return DB.GetAllUsers();
            }
            throw new ArgumentException("nenhuma pessoa encontrada");
        }
        public async Task ServiceDeleteForId(int id)
        {
            if (!DB.PersonExist(id))
            {
                throw new ArgumentException("Nao tem como deletar alguem que não existe no banco.");
            }
            if (Valid.IdValidation(id))
            {
                DB.DeleteForId(id);
                return;
            }
        }
        public async Task ServiceUpdatePersonId(int id, string nome, int idade, string cpf)
        {
            if (!DB.PersonExist(id))
            {
                throw new ArgumentException("Nao tem como atualizar uma pessoa que não existe no banco");
            }
            if (Valid.IdValidation(id))
            {
                DB.UpdatePersonId(id, nome, idade, cpf);
                return;
            }

        }
    }
}


