using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Command.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepositoriy _projectRepositoriy;


        public CreateProjectCommandHandler(IProjectRepositoriy projectRepositoriy)
        {
            _projectRepositoriy = projectRepositoriy;
        }


        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            //Preencho o objeto
            var project = new Project(
                request.Title,
                request.Description,
                request.IdClient,
                request.IdFreelancer,
                request.TotalCost);

            //Passo o obejeto para repository
            await _projectRepositoriy.AddProject(project);
            
            //Retorno Void
            return project.Id;
        }
    }
}
