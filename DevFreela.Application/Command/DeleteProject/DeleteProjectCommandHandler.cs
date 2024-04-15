using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Command.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {

        private readonly IProjectRepositoriy _repository;

        public DeleteProjectCommandHandler(IProjectRepositoriy repositoriy)
        {
            _repository = repositoriy;
        }

        public async  Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {

                var project = await _repository.GetByIdAsync(request.Id);

                project.Cancel();

                await _repository.SaveChangesAsync();
               
                //var projetc = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
                //projetc.Cancel();
                //await _dbContext.SaveChangesAsync();

                return Unit.Value;
            
        }
    }
}
