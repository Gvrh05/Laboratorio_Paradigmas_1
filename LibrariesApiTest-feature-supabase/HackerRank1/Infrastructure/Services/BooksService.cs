using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryService.WebAPI.Application.Interfaces;
using LibraryService.WebAPI.Domain.Entities;
using LibraryService.WebAPI.Domain.Interfaces;

namespace LibraryService.WebAPI.Infrastructure.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBookRepository _bookRepository;

        public BooksService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> Get(int libraryId, int[] ids)
        {
            return await _bookRepository.GetByLibraryIdAsync(libraryId, ids);
        }

        public async Task<Book> Add(Book book)
        {
            return await _bookRepository.AddAsync(book);
        }

        public async Task<Book> Update(Book book)
        {
            return await _bookRepository.UpdateAsync(book);
        }

        public async Task<bool> Delete(Book book)
        {
            return await _bookRepository.DeleteAsync(book);
        }
    }
}