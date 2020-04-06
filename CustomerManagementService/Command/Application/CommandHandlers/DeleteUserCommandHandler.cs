using AutoMapper;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.CustomerManagementService.Command.Application.Event;
using OMF.CustomerManagementService.Command.Application.Repositories;
using ServiceBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace OMF.CustomerManagementService.Command.Application.CommandHandlers
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEventBus _bus;
        private readonly IMapper _map;

        public DeleteUserCommandHandler(IAuthRepository authRepository, IEventBus bus, IMapper map)
        {
            _authRepository = authRepository;
            _bus = bus;
            _map = map;
        }
        public async Task HandleAsync(DeleteUserCommand command)
        {
            try
            {
                command.Email = command.Email.ToLower();
                if (!await _authRepository.UserExists(command.Email))
                {
                    await _bus.PublishEvent(new ExceptionEvent("user_doesn't_exists", $"Email: {command.Email} is not present in the system", command));
                }

                var userToDelete = _map.Map<User>(command);

                await _authRepository.DeleteUser(userToDelete, command.Password);

            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception", $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}
