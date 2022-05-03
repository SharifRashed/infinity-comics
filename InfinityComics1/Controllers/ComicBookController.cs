using System;
using System.Collections.Generic;
using System.Security.Claims;
using InfinityComics1.Models;
using InfinityComics1.Models.ViewModels;
using InfinityComics1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfinityComics1.Controllers
{
    public class ComicBookController : Controller
    {
       
        private readonly IComicBookRepository _comicBookRepository;
        private readonly IAuthorRepository _authorRepository;
        public ComicBookController(IComicBookRepository comicBookRepository, IAuthorRepository authorRepository)
        {
            _comicBookRepository = comicBookRepository;
            _authorRepository = authorRepository;
        }
        public IActionResult Index()
        {
            int userProfileId = GetCurrentUserId();
            List<ComicBook> comicBooks = _comicBookRepository.GetAllComicBooks();
            return View(comicBooks);
        }

        // GET: ComicBookController/Create
        public ActionResult Create()
        {
            List<Author> authors = _authorRepository.GetAllAuthors();

            ComicBookFormViewModel vm = new ComicBookFormViewModel()
            {               
                Authors = authors
            };

            return View(vm);
        }


        //POST: ComicBookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComicBookFormViewModel vm)
        {
            try
            {
                vm.ComicBook.UserProfileId = GetCurrentUserId();

                _comicBookRepository.AddComicBook(vm.ComicBook);

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                List<Author> authors = _authorRepository.GetAllAuthors();
                vm.Authors = authors;
              

                return View(vm);
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
