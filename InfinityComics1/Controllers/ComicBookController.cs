using System.Collections.Generic;
using InfinityComics1.Auth.Models;
using InfinityComics1.Models;
using InfinityComics1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfinityComics1.Controllers
{
    public class ComicBookController : Controller
    {

        private readonly IComicBookRepository _comicBookRepository;
        public ComicBookController(IComicBookRepository comicBookRepository)
        {
            _comicBookRepository = comicBookRepository;
        }
        public IActionResult Index()
        {
            List<ComicBook> comicBooks = _comicBookRepository.GetAllComicBooks();
            return View(comicBooks);
        }
    }
}
