using MySpace.Api.Application.Exceptions;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public class TagService : ITagService
{
    private readonly TagRepository _tagRepository;

    public TagService(TagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public IEnumerable<Tag> GetTags()
    {
        return _tagRepository.GetTags();
    }

    public Tag AddTag(Tag tag)
    {
        return TagExists(tag) ? tag : _tagRepository.AddTag(tag);
    }

    public void DeleteTag(Tag tag)
    {
        ThrowExceptionIfTagNotFound(tag);
        _tagRepository.DeleteTag(tag);
    }

    public bool TagExists(Tag tag)
    {
        return _tagRepository.TagExists(tag);
    }

    private void ThrowExceptionIfTagNotFound(Tag tag)
    {
        if (!TagExists(tag))
        {
            throw new TagNotFoundException(tag.ToString());
        }
    }
}