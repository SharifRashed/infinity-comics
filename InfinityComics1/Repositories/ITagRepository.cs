using InfinityComics1.Models;
using System.Collections.Generic;

namespace InfinityComics1.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();     
        Tag GetTagById(int id);

        public void SaveComicTag(int TagId, int ComicBookId);  
    }
}
