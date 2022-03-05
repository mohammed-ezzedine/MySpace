using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface ITagService
{
    List<Tag> GetTags();
    Tag AddTag(Tag tag);
    Tag UpdateTag(Tag tag);
    bool TagExists(Tag tag);
}