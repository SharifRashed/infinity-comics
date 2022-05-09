using InfinityComics1.Models.ViewModels;
using InfinityComics1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InfinityComics1.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IComicBookRepository _comicBookRepository;
        #region tag
        public TagController(ITagRepository tagRepository, IComicBookRepository comicBookRepository)
        {
            _comicBookRepository = comicBookRepository;
            _tagRepository = tagRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            
            return View();
        }

        public ActionResult Create(int id)
        {
           
            return View();
        }
        #endregion tag
    }
}
