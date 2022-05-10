using System.Collections.Generic;
using InfinityComics1.Models;

namespace InfinityComics1.Repositories
{
    public interface IComicBookRepository
    {
        void AddComicBook(ComicBook comicBook);
        List<ComicBook> GetAllComicBooks(int id);       
        ComicBook GetComicBookById(int id);      
        void Delete(int id);
       void Update(ComicBook comicBook);

    }
}
