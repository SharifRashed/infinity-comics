using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfinityComics1.Controllers
{
    [Authorize]
    public class ComicBookController : Controller
    {

        private readonly IComicBookRepository _comicBookRepository;
        public IActionResult Index()
        {
            var comicBook = _comicBookRepository.GetAllComicBooks();
            return View(comicBooks);
        }
    }
}
