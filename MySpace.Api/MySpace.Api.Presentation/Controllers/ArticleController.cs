using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySpace.Api.Application.Services;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Filters;
using MySpace.Api.Presentation.Requests;
using MySpace.Api.Presentation.Responses;

namespace MySpace.Api.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public ArticleController(IArticleService articleService, IMapper mapper)
    {
        _articleService = articleService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(List<ArticleResponse>))]
    public ActionResult<List<ArticleResponse>> GetArticles(string? q, string? tag)
    {
        var articles = q != null
            ? _articleService.QueryArticles(q)
            : tag != null 
                ? _articleService.GetArticlesByTag(new Tag(tag))
                : _articleService.GetArticles();
        var articleResponses = articles.Select(_mapper.Map<ArticleResponse>).ToList();
        return Ok(articleResponses);
    }
    
    [HttpGet("{id:int}")]
    [ArticleNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ArticleResponse))]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public ActionResult<ArticleResponse> GetArticle(int id)
    {
        var article = _articleService.GetArticle(id);
        return Ok(_mapper.Map<ArticleResponse>(article));
    }

    [Authorize]
    [HttpPost]
    [EnableCors]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(ArticleResponse))]
    public ActionResult<ArticleResponse> AddArticle(ArticleRequest article)
    {
        var result = _articleService.AddArticle(_mapper.Map<Article>(article));
        return Created(result.Id.ToString(), _mapper.Map<ArticleResponse>(result));
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ArticleResponse))]
    public ActionResult<ArticleResponse> UpdateArticle(int id, ArticleRequest article)
    {
        var result = _articleService.UpdateArticle(id, _mapper.Map<Article>(article));
        return Ok(_mapper.Map<ArticleResponse>(result));
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ArticleNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public IActionResult DeleteArticle(int id)
    {
        _articleService.DeleteArticle(id);
        return NoContent();
    }
}