using MySpace.Api.Domain.Models;
using MySpace.Api.Domain.Persistence;

namespace MySpace.Api.Application.Services;

public class TagService : ITagService
{
    private readonly TagRepository _tagRepository;

    public TagService(TagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public List<Tag> GetTags()
    {
        return _tagRepository.GetTags();
    }

    public Tag AddTag(Tag tag)
    {
        return TagExists(tag) ? tag : _tagRepository.AddTag(tag);
    }

    public Tag UpdateTag(Tag tag)
    {
        return _tagRepository.UpdateTag(tag);
    }

    public bool TagExists(Tag tag)
    {
        return _tagRepository.TagExists(tag);
    }
}