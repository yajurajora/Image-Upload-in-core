using ImageUploadInCore.Common.Abstraction;
using ImageUploadInCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUploadInCore.Service
{
    public class PersonService : IPersonService
    {
        public readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<Persons> AddPerson(Persons persons)
        {
            return await _personRepository.AddAsync(persons);
        }

        public async Task<bool> DeletePerson(string id)
        {
            return await _personRepository.DeleteAsync(new Persons()
            { PID = id });
        }

        public async Task<ICollection<Persons>> GetAllPerson()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Persons> GetPersonById(string personId)
        {
            return await _personRepository.GetByIdAsync(personId);
        }

        public async Task<Persons> UpdatePerson(Persons persons, string id)
        {
            return await _personRepository.UpdateAsync(persons, id);
        }
    }
}
