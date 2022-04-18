using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Persistence;

public interface TagRepository
{
    List<Tag> GetTags(int total = 20);
    Tag AddTag(Tag tag);
    bool TagExists(Tag tag);
    void DeleteTag(Tag tag);
    void IncrementTagArticlesCounter(string tagName);
    void DecrementTagArticlesCounter(string tagName);
}