using System.Net;
using AutoMapper;
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
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;
    private readonly HashIdUtil _hashIdUtil;

    public CommentController(ICommentService commentService, IMapper mapper, HashIdUtil hashIdUtil)
    {
        _commentService = commentService;
        _mapper = mapper;
        _hashIdUtil = hashIdUtil;
    }

    [HttpGet("{articleId}/{commentId}")]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> GetComment(string articleId, string commentId)
    {
        var comment = _commentService.GetComment(_hashIdUtil.DecodeId(articleId), commentId);
        return Ok(_mapper.Map<CommentResponse>(comment));
    }

    [HttpPost("{articleId}")]
    [ArticleNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> PostCommentOnArticle(string articleId, CommentRequest comment)
    {
        var addedComment = _commentService.AddCommentToArticle(_hashIdUtil.DecodeId(articleId), _mapper.Map<Comment>(comment));
        return Created(articleId + "/" + addedComment.Id, _mapper.Map<CommentResponse>(addedComment));
    }
    
    [HttpPost("{articleId}/{commentId}")]
    [ArticleNotFoundExceptionFilter]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> PostReplyOnComment(string articleId, string commentId, CommentRequest comment)
    {
        var addedComment = _commentService.AddReplyToComment(_hashIdUtil.DecodeId(articleId), commentId, _mapper.Map<Comment>(comment));
        return Created(articleId + "/" + addedComment.Id, _mapper.Map<CommentResponse>(addedComment));
    }
    
    [HttpPut("{articleId}/{commentId}")]
    [ArticleNotFoundExceptionFilter]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(CommentResponse))]
    public ActionResult<CommentResponse> EditComment(string articleId, string commentId, CommentRequest comment)
    {
        var addedComment = _commentService.EditComment(_hashIdUtil.DecodeId(articleId), commentId, _mapper.Map<Comment>(comment));
        return Ok(_mapper.Map<CommentResponse>(addedComment));
    }
    
    [HttpDelete("{articleId}/{commentId}")]
    [ArticleNotFoundExceptionFilter]
    [CommentNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public ActionResult<CommentResponse> DeleteComment(string articleId, string commentId)
    {
        _commentService.DeleteComment(_hashIdUtil.DecodeId(articleId), commentId);
        return NoContent();
    }
}