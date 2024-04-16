using DevFreela.API.Models;
using DevFreela.Application.Command.CreateUserCommand;
using DevFreela.Application.Command.LoginUser;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;


        public UsersController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }


        //api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody]CreateUserCommand command)
        {
            var id = await  _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = id }, command);

        }

        //api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var command = new GetUserQuery(id);
            var userViewModel = await _mediator.Send(command);

            return Ok(userViewModel);

            //Buscar, se não existir, retorna NotFound
            //var user = _userService.GetUser(id);

            //if(user == null)
            //{
            //    return NotFound();
            //}
            
            //return Ok(user);
        }

        //api/users/{id}
        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginViewModel = await _mediator.Send(command);

            if(loginViewModel is null)
                return BadRequest();


            return Ok(loginViewModel);
        }
    }
}
