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
using MySpace.Api.Presentation.Utils;

namespace MySpace.Api.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;
    private readonly HashIdUtil _hashIdUtil;

    public ArticleController(IArticleService articleService, IMapper mapper, HashIdUtil hashIdUtil)
    {
        _articleService = articleService;
        _mapper = mapper;
        _hashIdUtil = hashIdUtil;
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
        var articleResponses = articles.Select(MapToArticleResponse).ToList();
        return Ok(articleResponses);
    }
    
    [HttpGet("{id}")]
    [ArticleNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ArticleResponse))]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public ActionResult<ArticleResponse> GetArticle(string id)
    {
        var article = _articleService.GetArticle(_hashIdUtil.DecodeId(id));
        return Ok(MapToArticleResponse(article));
    }

    [Authorize]
    [HttpPost]
    [EnableCors]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(ArticleResponse))]
    public ActionResult<ArticleResponse> AddArticle(ArticleRequest article)
    {
        var result = _articleService.AddArticle(_mapper.Map<Article>(article));
        return Created(_hashIdUtil.EncodeId(result.Id.Value), MapToArticleResponse(result));
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ArticleResponse))]
    public ActionResult<ArticleResponse> UpdateArticle(string id, ArticleRequest article)
    {
        var result = _articleService.UpdateArticle(_hashIdUtil.DecodeId(id), _mapper.Map<Article>(article));
        return Ok(MapToArticleResponse(result));
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ArticleNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public IActionResult DeleteArticle(string id)
    {
        _articleService.DeleteArticle(_hashIdUtil.DecodeId(id));
        return NoContent();
    }

    private ArticleResponse MapToArticleResponse(Article article)
    {
        var articleResponse = _mapper.Map<ArticleResponse>(article);
        articleResponse.Id = _hashIdUtil.EncodeId(article.Id.Value);
        return articleResponse;
    }
}