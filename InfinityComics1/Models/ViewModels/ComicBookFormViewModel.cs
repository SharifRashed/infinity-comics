using InfinityComics1.Models;
using System.Collections.Generic;
using System;

namespace InfinityComics1.Models.ViewModels
{
    public class ComicBookFormViewModel
    {
        public ComicBook ComicBook { get; set; }
        public List<Author> Authors { get; set; }

        public List<Tag> Tags { get; set; }
        public List<int> SelectedTagIds { get; set; }
    }
}
