using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.CustomerManagementService.Command.Repository.Abstractions;
using OMF.CustomerManagementService.Command.Repository.DataContext;
using OMF.CustomerManagementService.Command.Service.Command;
using OMF.CustomerManagementService.Command.Service.Event;
using ServiceBus.Abstractions;

namespace OMF.CustomerManagementService.Command.Service.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _map;

        public CreateUserCommandHandler(IAuthRepository authRepository, IMapper map)
        {
            _authRepository = authRepository;
            _map = map;
        }

        /// <summary>
        /// Create user command handler
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<Response> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            command.Email = command.Email.ToLower();
            var userToCreate = _map.Map<TblCustomer>(command);

            if (await _authRepository.UserExists(command.Email))
                return new Response(400, $"Email: {command.Email} is already in use");

            var createdUser = await _authRepository.Register(userToCreate, command.Password);

            return new Response(200, "User registered succesfully");

        }

    }
}