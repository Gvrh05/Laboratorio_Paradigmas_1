using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryService.WebAPI.Application.Interfaces;
using LibraryService.WebAPI.Domain.Entities;
using LibraryService.WebAPI.Domain.Interfaces;

namespace LibraryService.WebAPI.Infrastructure.Services
{
    public class LibrariesService : ILibrariesService
    {
        private readonly ILibraryRepository _libraryRepository;

        public LibrariesService(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public async Task<IEnumerable<Library>> Get(int[] ids)
        {
            return await _libraryRepository.GetByIdsAsync(ids);
        }

        public async Task<Library> Add(Library library)
        {
            return await _libraryRepository.AddAsync(library);
        }

        public async Task<Library> Update(Library library)
        {
            return await _libraryRepository.UpdateAsync(library);
        }

        public async Task<bool> Delete(Library library)
        {
            return await _libraryRepository.DeleteAsync(library);
        }
    }
}