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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _map;

        public UpdateUserCommandHandler(IAuthRepository authRepository, IMapper map)
        {
            _authRepository = authRepository;
            _map = map;
        }

        public async Task<Response> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {

            var updatedUser = _map.Map<TblCustomer>(command);
            updatedUser.ModifiedDate = DateTime.UtcNow;
            updatedUser.Active = true;
            await _authRepository.UpdateUser(updatedUser, command.Password);

            return new Response(200,"User updated successfully");

        }
    }
}