using ImageUploadInCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUploadInCore.Common.Abstraction
{
    public interface IPersonRepository
    {
        Task<Persons> AddAsync(Persons persons);
        Task<Persons> GetByIdAsync(string personId);
        Task<Persons> UpdateAsync(Persons persons, string id);
        Task<ICollection<Persons>> GetAllAsync();
        Task<bool> DeleteAsync(Persons persons);
    }
}
