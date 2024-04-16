using MediatR;

namespace DevFreela.Application.Command.CreateUserCommand
{
    public class CreateUserCommand:IRequest<int>
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
    }
}
