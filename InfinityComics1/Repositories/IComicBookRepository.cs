using System.Collections.Generic;
using InfinityComics1.Auth.Models;
using InfinityComics1.Models;

namespace InfinityComics1.Repositories
{
    public interface IComicBookRepository
    {
        void AddComicBook(ComicBook comicBook);
        List<ComicBook> GetAllComicBooks();
    }
}
