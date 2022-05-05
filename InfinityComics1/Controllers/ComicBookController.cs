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

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        private readonly IComicBookRepository _comicBookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ITagRepository _tagRepository;
        public ComicBookController(IComicBookRepository comicBookRepository, IAuthorRepository authorRepository, ITagRepository tagRepository)
        {
            _comicBookRepository = comicBookRepository;
            _authorRepository = authorRepository;
            _tagRepository = tagRepository;
        }
        public IActionResult Index()
        {
            int userProfileId = GetCurrentUserId();
            List<ComicBook> comicBooks = _comicBookRepository.GetAllComicBooks();
            return View(comicBooks);
        }

        public ActionResult Details(int id)
        {
            
            //ComicBook comicBook = _comicBookRepository.GetComicBookById(id);
            List<Tag> tags = _tagRepository.GetAllTags();
            ComicBookFormViewModel vm = new ComicBookFormViewModel()
            {
                Tags = tags
            };
          
            if (vm == null)
            {
                return NotFound();
            } 
            return View(vm);
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
        //GET: ComicBook/Edit/1
        public ActionResult Edit(int id)
        {
            ComicBook comicBook = _comicBookRepository.GetComicBookById(id);
            ComicBookFormViewModel comicBookFormViewModel = new ComicBookFormViewModel()
            {
                ComicBook = comicBook,
                Authors = _authorRepository.GetAllAuthors(),
            };

            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBookFormViewModel);
        }

        //POST: ComicBook/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ComicBookFormViewModel comicBookFormViewModel)
        {
            try
            {
                comicBookFormViewModel.ComicBook.UserProfileId = GetCurrentUserId();
                _comicBookRepository.Update(comicBookFormViewModel.ComicBook);
              
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(comicBookFormViewModel.ComicBook);
            }
        }

        // GET: ComicBookController/Delete/1
        public ActionResult Delete(int id)
        {
            ComicBook comicBook = _comicBookRepository.GetComicBookById(id);
            if (comicBook != null)
            {
                return View(comicBook);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ComicBook comicBook)
        {
            try
            {
                _comicBookRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(comicBook);
            }


        }
    }
}
