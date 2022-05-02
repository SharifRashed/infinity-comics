using System;
using System.Collections.Generic;
using System.Security.Claims;
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
            int userProfileId = GetCurrentUserId();
            List<ComicBook> comicBooks = _comicBookRepository.GetAllComicBooks();
            return View(comicBooks);
        }

        // GET: ComicBookController/Create
        public ActionResult Create()
        {
            return View();
        }


        //POST: ComicBookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComicBook comicBook)
        {
            try
            {
                comicBook.UserProfileId = GetCurrentUserId();

                _comicBookRepository.AddComicBook(comicBook);

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                return View(comicBook);
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
