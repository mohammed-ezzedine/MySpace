using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpace.Api.Application.Services;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Filters;
using MySpace.Api.Presentation.Responses;

namespace MySpace.Api.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;

    public TagController(ITagService tagService, IMapper mapper)
    {
        _tagService = tagService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(List<string>))]
    public ActionResult<List<string>> GetTags()
    {
        var tagResponses = _tagService.GetTags()
            .Select(t => t.Name)
            .ToList();
        return Ok(tagResponses);
    }

    [Authorize]
    [HttpDelete("{name}")]
    [TagNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public IActionResult DeleteTag(string name)
    {
        _tagService.DeleteTag(new Tag(name));
        return NoContent();
    }
}