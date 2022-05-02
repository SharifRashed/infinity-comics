using InfinityComics1.Models;
using System.Collections.Generic;

namespace InfinityComics1.Repositories
{
    public interface IAuthorRepository
    {
        List<Author> GetAllAuthors();
    }
}
