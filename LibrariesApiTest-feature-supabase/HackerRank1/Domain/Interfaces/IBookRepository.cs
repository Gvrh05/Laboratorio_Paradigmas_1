using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryService.WebAPI.Domain.Entities;

namespace LibraryService.WebAPI.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetByLibraryIdAsync(int libraryId, int[] ids);

        Task<Book> GetByIdAsync(int id);

        Task<Book> AddAsync(Book book);

        Task<Book> UpdateAsync(Book book);

        Task<bool> DeleteAsync(Book book);
    }
}