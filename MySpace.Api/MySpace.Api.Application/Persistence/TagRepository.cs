using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Persistence;

public interface TagRepository
{
    List<Tag> GetTags();
    Tag AddTag(Tag tag);
    bool TagExists(Tag tag);
    void DeleteTag(Tag tag);
}