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
using ServiceBus.Abstractions;

namespace OMF.CustomerManagementService.Command.Service.CommandHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand,Response>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _map;

        public DeleteUserCommandHandler(IAuthRepository authRepository, IMapper map)
        {
            _authRepository = authRepository;
            _map = map;
        }

        /// <summary>
        /// Deactivate user command handler
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<Response> Handle(DeleteUserCommand command,CancellationToken cancellationToken)
        {

            command.Email = command.Email.ToLower();

            if (!await _authRepository.UserExists(command.Email))
                return new Response(400, $"Email: {command.Email} is not present in the system");

            var userToDelete = _map.Map<TblCustomer>(command);

            await _authRepository.DeleteUser(userToDelete, command.Password);

            return new Response(200,"User deactivated successfully");

        }
    }
}