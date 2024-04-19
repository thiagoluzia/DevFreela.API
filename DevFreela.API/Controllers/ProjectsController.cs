//using DevFreela.API.Models;
using DevFreela.API.Models;
using DevFreela.Application.Command.CreateComment;
using DevFreela.Application.Command.CreateProject;
using DevFreela.Application.Command.DeleteProject;
using DevFreela.Application.Command.FinishProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjetcById;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "client, freelancer")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public ProjectsController(IProjectService projectService, IMediator mediator)
        {
            _projectService = projectService;
            _mediator = mediator;
        }


        //api/projects
        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand comamnd)
        {

            if (comamnd.Title.Length > 50)
            {
                return BadRequest();
            }
            var id = await _mediator.Send(comamnd);

            //Cadastra o Objeto

            return CreatedAtAction(nameof(GetById), new { id }, comamnd);
        }

        //api/projects?query=net core
        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            //Filtrar ou consultar o objeto
            var view = new GetAllProjectsQuery();

            var projects = await _mediator.Send(view);

            if (projects == null)
            {
                return NotFound("Nenhum projeto encontrado!");
            }

            return Ok(projects);
        }

        //api/projects/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //Buscar, se não existir, retorna NotFound
            var query = new GetProjectByIdQuery(id);

            var project = await _mediator.Send(query);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
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
        public async Task<IActionResult> Delete(int id)
        {
            //Buscar, se não existir, retorna NotFound
            var project = new DeleteProjectCommand(id);

            if (project == null)
            {
                return NotFound();
            }

            //Remover
            await _mediator.Send(project);

            return NoContent();
        }

        //api/projects/{id}/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = "freelancer")]
        public IActionResult PutStart(int id)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized("Usuario não altenticado.");
            //}

            if (!User.IsInRole("freelancer"))
            {
                return Forbid("Usuario não possui permissão.");
            }

            _projectService.Start(id);

            return NoContent();
        }

        //api/projects/{id}/finish
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id, [FromBody]FinishProjectCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("O pagamento não pôde ser processado.");
            }

           // _projectService.Finish(id);
            return Accepted();
        }

        //api/projects/{id}/comments
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostCopmment(int id, [FromBody] CreateCommentCommand inputModel)
        {
            inputModel.IProject = id;

            await _mediator.Send(inputModel);

            return NoContent();
        }

    }
}
