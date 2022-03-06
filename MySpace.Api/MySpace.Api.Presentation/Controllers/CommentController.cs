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

    [HttpGet("{articleId:guid}/{commentId:guid}")]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> GetComment(Guid articleId, Guid commentId)
    {
        var comment = _commentService.GetComment(new ArticleId(articleId), new CommentId(commentId));
        return Ok(_mapper.Map<CommentResponse>(comment));
    }

    [HttpPost("{articleId:guid}")]
    [ArticleNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> PostCommentOnArticle(Guid articleId, CommentRequest comment)
    {
        var addedComment = _commentService.AddCommentToArticle(new ArticleId(articleId), _mapper.Map<Comment>(comment));
        return Created(addedComment.Id.ToString(), _mapper.Map<CommentResponse>(addedComment));
    }
    
    [HttpPost("{articleId:guid}/{commentId:guid}")]
    [ArticleNotFoundExceptionFilter]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> PostReplyOnComment(Guid articleId, Guid commentId, CommentRequest comment)
    {
        var addedComment = _commentService.AddReplyToComment(new ArticleId(articleId), new CommentId(commentId), _mapper.Map<Comment>(comment));
        return Created(addedComment.Id.ToString(), _mapper.Map<CommentResponse>(addedComment));
    }
    
    [HttpPut("{articleId:guid}/{commentId:guid}")]
    [ArticleNotFoundExceptionFilter]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> EditComment(Guid articleId, Guid commentId, CommentRequest comment)
    {
        var addedComment = _commentService.EditComment(new ArticleId(articleId), new CommentId(commentId), _mapper.Map<Comment>(comment));
        return Ok(_mapper.Map<CommentResponse>(addedComment));
    }
    
    [HttpDelete("{articleId:guid}/{commentId:guid}")]
    [ArticleNotFoundExceptionFilter]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public ActionResult<CommentResponse> DeleteComment(Guid articleId, Guid commentId)
    {
        _commentService.DeleteComment(new ArticleId(articleId), new CommentId(commentId));
        return NoContent();
    }
}