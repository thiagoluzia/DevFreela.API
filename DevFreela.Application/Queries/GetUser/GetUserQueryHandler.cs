using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IUsersRepository _repository;

        public GetUserQueryHandler(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await  _repository.GetByIdAsync(request.Id);

            if (user is null) 
                return null;

            return new UserViewModel(user.FullName, user.Email);
        }
    }
}
