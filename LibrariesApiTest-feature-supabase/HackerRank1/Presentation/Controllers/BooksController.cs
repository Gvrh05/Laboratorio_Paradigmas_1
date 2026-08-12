using System.Threading.Tasks;
using LibraryService.WebAPI.Application.Interfaces;
using LibraryService.WebAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.WebAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService, ILibrariesService librariesService)
        {
            _librariesService = librariesService;
            _booksService = booksService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int libraryId)
        {
            var books = await _booksService.Get(libraryId, null);
            return Ok(books);
        }
    }
}