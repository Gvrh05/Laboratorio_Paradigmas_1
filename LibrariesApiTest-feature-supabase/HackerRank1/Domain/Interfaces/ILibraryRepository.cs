using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryService.WebAPI.Domain.Entities;

namespace LibraryService.WebAPI.Domain.Interfaces
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<Library>> GetByIdsAsync(int[] ids);

        Task<IEnumerable<Library>> GetAllAsync();

        Task<Library> AddAsync(Library library);

        Task<Library> UpdateAsync(Library library);

        Task<bool> DeleteAsync(Library library);
    }
}