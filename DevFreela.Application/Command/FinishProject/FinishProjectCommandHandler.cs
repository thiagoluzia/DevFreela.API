﻿using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Command.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepositoriy _repository;
        private readonly IPaymentService _paymentService;



        public FinishProjectCommandHandler(IProjectRepositoriy repository, IPaymentService paymentService)
        {
            _repository = repository;
            _paymentService = paymentService;
        }


        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {

            var project = await _repository.GetByIdAsync(request.Id);

            var paymentInfoDto = new PaymentInfoDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, project.TotalCost);

            _paymentService.ProccessPayment(paymentInfoDto);

            project.SetPaymentPending();

            await _repository.SaveChangesAsync();

            return true;

        }
    }
}
