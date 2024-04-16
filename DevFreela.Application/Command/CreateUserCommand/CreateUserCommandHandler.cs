using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Command.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUsersRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Gera o hash da senha fornecida usando o serviço de autenticação
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            // Cria uma nova instância de usuário com os dados fornecidos
            var user = new User(request.FullName, request.Email, request.BirthDate, request.Role, passwordHash);

            // Adiciona o novo usuário ao repositório
            await _repository.AddUser(user);

            // Retorna o ID do novo usuário criado
            return user.Id;

        }
    }
}
