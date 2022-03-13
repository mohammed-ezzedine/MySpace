using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpace.Authentication.Application.Services;
using MySpace.Authentication.Presentation.Filters;
using MySpace.Authentication.Presentation.Requests;
using MySpace.Authentication.Presentation.Responses;

namespace MySpace.Authentication.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;

    public IdentityController(IAuthenticationService authenticationService, IMapper mapper)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(List<UserResponse>))]
    public ActionResult<List<UserResponse>> GetUsers()
    {
        var users = _authenticationService.GetUsers()
            .Select(_mapper.Map<UserResponse>)
            .ToList();

        return Ok(users);
    }

    [Authorize]
    [HttpPost("register")]
    [UsernameAlreadyExistsExceptionFilter]
    [RegisteringUserFailedExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public async Task<ActionResult> Register(RegisterRequest request)
    {
        await _authenticationService.RegisterUser(request.Username, request.Password);
        return NoContent();
    }

    [HttpPost("login")]
    [UserNotFoundExceptionFilter]
    [InvalidCredentialExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(string))]
    public async Task<ActionResult<string>> Login(LoginRequest request)
    {
        var token = await _authenticationService.Login(request.Username, request.Password);
        return Ok(token);
    }

    [Authorize]
    [HttpPut]
    [UserNotFoundExceptionFilter]
    [InvalidCredentialExceptionFilter]
    [ChangePasswordFailedExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
    {
        await _authenticationService.ChangePassword(request.Username, request.CurrentPassword, request.NewPassword);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{username}")]
    [UserNotFoundExceptionFilter]
    [DeleteUserFailedExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult> DeleteUser(string username)
    {
        await _authenticationService.DeleteUser(username);
        return NoContent();
    }
}