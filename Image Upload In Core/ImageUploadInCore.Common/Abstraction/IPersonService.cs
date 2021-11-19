using ImageUploadInCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUploadInCore.Common.Abstraction
{
    public interface IPersonService
    {
        Task<ICollection<Persons>> GetAllPerson();
        Task<Persons> GetPersonById(string personId);
        Task<Persons> UpdatePerson(Persons persons, string id);
        Task<Persons> AddPerson(Persons persons);
        Task<bool> DeletePerson(string id);
    }
}
