using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MySpace.Api.Application.Services;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Filters;
using MySpace.Api.Presentation.Requests;
using MySpace.Api.Presentation.Responses;

namespace MySpace.Api.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet("{articleId:int}/{commentId}")]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> GetComment(int articleId, string commentId)
    {
        var comment = _commentService.GetComment(articleId, commentId);
        return Ok(_mapper.Map<CommentResponse>(comment));
    }

    [HttpPost("{articleId:int}")]
    [ArticleNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> PostCommentOnArticle(int articleId, CommentRequest comment)
    {
        var addedComment = _commentService.AddCommentToArticle(articleId, _mapper.Map<Comment>(comment));
        return Created(addedComment.Id, _mapper.Map<CommentResponse>(addedComment));
    }
    
    [HttpPost("{articleId:int}/{commentId}")]
    [ArticleNotFoundExceptionFilter]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> PostReplyOnComment(int articleId, string commentId, CommentRequest comment)
    {
        var addedComment = _commentService.AddReplyToComment(articleId, commentId, _mapper.Map<Comment>(comment));
        return Created(addedComment.Id, _mapper.Map<CommentResponse>(addedComment));
    }
    
    [HttpPut("{articleId:int}/{commentId}")]
    [ArticleNotFoundExceptionFilter]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> EditComment(int articleId, string commentId, CommentRequest comment)
    {
        var addedComment = _commentService.EditComment(articleId, commentId, _mapper.Map<Comment>(comment));
        return Ok(_mapper.Map<CommentResponse>(addedComment));
    }
    
    [HttpDelete("{articleId:int}/{commentId}")]
    [ArticleNotFoundExceptionFilter]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public ActionResult<CommentResponse> DeleteComment(int articleId, string commentId)
    {
        _commentService.DeleteComment(articleId, commentId);
        return NoContent();
    }
}