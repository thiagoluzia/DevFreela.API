using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        //api/users
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel user)
        {
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        //api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //Buscar, se não existir, retorna NotFound
            
            return Ok();
        }

        //api/users/{id}
        [HttpPut("{id}")]
        public IActionResult Login(int id, [FromBody] LoginModel login)
        {
            return NoContent();
        }
    }
}
