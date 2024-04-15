using MediatR;

namespace DevFreela.Application.Command.CreateComment
{
    public class CreateCommentCommand : IRequest<Unit>
    {
        public string Content { get; set; }
        public int IProject { get; set; }
        public int IdUser { get; set; }
    }
}
