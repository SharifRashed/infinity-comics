using InfinityComics1.Auth.Models;
using System.Collections.Generic;

namespace InfinityComics1.Models.ViewModels
{
    public class ComicBookFormViewModel
    {
        public ComicBook ComicBook { get; set; }
        public List<Author> Authors { get; set; }
    }
}
