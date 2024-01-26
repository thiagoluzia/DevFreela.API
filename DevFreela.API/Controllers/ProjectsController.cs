//using DevFreela.API.Models;
using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        //api/projects?query=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            //Filtrar ou consultar o objeto

            var projects = _projectService.GetAll(query);

            return Ok(projects);
        }

        //api/projects/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //Buscar, se não existir, retorna NotFound
            var project = _projectService.GetById(id);

            if(project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        //api/projects
        [HttpPost]
        public IActionResult Post([FromBody] NewProjectInputModel inputModel) 
        {
            
            if(inputModel.Title.Length > 50)
            {
                return BadRequest();
            }
            var id = _projectService.Create(inputModel);

            //Cadastra o Objeto

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        
        [HttpPut("id")]
        public IActionResult Put([FromBody] UpdateProjectInputModel inputModel, int id)
        {
            if (inputModel.Description.Length > 200)
            {
                return BadRequest();
            }

            _projectService.Update(inputModel);

            //Atualiza o Objeto

            return NoContent();
        }

        //api/projects/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Buscar, se não existir, retorna NotFound
            var project = _projectService.GetById(id);

            if(project == null)
            {
                return NotFound();
            }

            //Remover
            _projectService.Delete(id);

            return NoContent();
        }

        //api/projects/{id}/start
        [HttpPut("{id}/start")]
        public IActionResult PutStart(int id)
        {
            _projectService.Start(id);

            return NoContent();
        }

        //api/projects/{id}/finish
        [HttpPut("{id}/finish")]
        public IActionResult PutFinish(int id)
        {
            _projectService.Finish(id);

            return NoContent();
        }

        /*
        //api/projects/{id}/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostCopmment(int id, [FromBody] CreateCommentModel comment)
        {
            return NoContent();
        }
        */
    }
}
