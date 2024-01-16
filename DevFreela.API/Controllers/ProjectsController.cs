using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        //api/projects?query=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            //Filtrar ou consultar o objeto
            return Ok();
        }

        //api/projects/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //Consulta o Objeto
            //return NotFound();

            return Ok();
        }

        //api/projects
        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectModel createProject) 
        {
            
            if(createProject.Title.Length > 50)
            {
                return BadRequest();
            }

            //Cadastra o Objeto

            return CreatedAtAction(nameof(GetById), new { id = createProject.Id }, createProject);
        }

        
        [HttpPut("id")]
        public IActionResult Put([FromBody] UpdateProjectModel updateProject, int id)
        {
            if (updateProject.Description.Length > 200)
            {
                return BadRequest();
            }

            //Atualiza o Objeto

            return NoContent();
        }

        //api/projects/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Buscar, se não existir, retorna NotFound
            
            //Remover

            return NoContent();
        }
    }
}
