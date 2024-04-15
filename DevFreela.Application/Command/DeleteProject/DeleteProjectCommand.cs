using MediatR;

namespace DevFreela.Application.Command.DeleteProject
{
    public class DeleteProjectCommand : IRequest<Unit>
    {
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }


        public int Id { get; private set; }
    }
}
