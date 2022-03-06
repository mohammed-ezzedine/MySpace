using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface ITagService
{
    IEnumerable<Tag> GetTags();
    Tag AddTag(Tag tag);
    void DeleteTag(Tag tag);
    bool TagExists(Tag tag);
}