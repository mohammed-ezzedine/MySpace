using MySpace.Api.Domain.Models;

namespace MySpace.Api.Domain.Persistence;

public interface TagRepository
{
    List<Tag> GetTags();
    Tag AddTag(Tag tag);
    Tag UpdateTag(Tag tag);
    bool TagExists(Tag tag);
}