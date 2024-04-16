using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Command.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IUsersRepository usersRepository, IAuthService authService)
        {
            _usersRepository = usersRepository;
            _authService = authService;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Utilizar o mesmo algoritmo para criar o hash dessa senha
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
           
            // Buscar no meu banco de dados um User que tenha meu e-mail e minha senha em formato hash
            var user = await _usersRepository.GetByEmailPasswordAsync(request.Email, passwordHash);

            // Se nao existir, erro no login
            if (user is null)
                return null;

            // Se existir, gero o token usando os dados do usuário
            var token = _authService.GenerationJwtToken(user.Email, user.Role);

            // Preencher a view model para retorno
            var loginViewModel = new LoginUserViewModel(user.Email, token);

            return loginViewModel;
        }
    }
}
