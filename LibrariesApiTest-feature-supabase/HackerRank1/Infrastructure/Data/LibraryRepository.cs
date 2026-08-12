using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.WebAPI.Domain.Entities;
using LibraryService.WebAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.WebAPI.Infrastructure.Data
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext _context;

        public LibraryRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            return await _context.Libraries.ToListAsync();
        }

        public async Task<IEnumerable<Library>> GetByIdsAsync(int[] ids)
        {
            var query = _context.Libraries.AsQueryable();

            if (ids != null && ids.Any())
                query = query.Where(x => ids.Contains(x.Id));

            return await query.ToListAsync();
        }

        public async Task<Library> AddAsync(Library library)
        {
            await _context.Libraries.AddAsync(library);
            await _context.SaveChangesAsync();
            return library;
        }

        public async Task<Library> UpdateAsync(Library library)
        {
            var entity = await _context.Libraries.SingleAsync(x => x.Id == library.Id);
            entity.Name = library.Name;
            entity.Location = library.Location;

            _context.Libraries.Update(entity);
            await _context.SaveChangesAsync();
            return library;
        }

        public async Task<bool> DeleteAsync(Library library)
        {
            _context.Libraries.Remove(library);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}