using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.WebAPI.Domain.Entities;
using LibraryService.WebAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.WebAPI.Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetByLibraryIdAsync(int libraryId, int[] ids)
        {
            var query = _context.Books.AsQueryable().Where(b => b.LibraryId == libraryId);

            if (ids != null && ids.Any())
                query = query.Where(b => ids.Contains(b.Id));

            return await query.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            var entity = await _context.Books.SingleAsync(x => x.Id == book.Id);
            entity.Name = book.Name;
            entity.Category = book.Category;
            entity.LibraryId = book.LibraryId;

            _context.Books.Update(entity);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}