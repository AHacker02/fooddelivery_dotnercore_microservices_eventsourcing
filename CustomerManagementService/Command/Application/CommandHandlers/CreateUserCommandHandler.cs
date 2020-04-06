using AutoMapper;
using OMF.Common.Events;
using OMF.Common.Models;
using OMF.CustomerManagementService.Command.Application.Command;
using OMF.CustomerManagementService.Command.Application.Event;
using OMF.CustomerManagementService.Command.Application.Repositories;
using ServiceBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace OMF.CustomerManagementService.Command.Application.CommandHandlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEventBus _bus;
        private readonly IMapper _map;

        public CreateUserCommandHandler(IAuthRepository authRepository, IEventBus bus, IMapper map)
        {
            _authRepository = authRepository;
            _bus = bus;
            _map = map;
        }
        public async Task HandleAsync(CreateUserCommand command)
        {
            try
            {
                command.Email = command.Email.ToLower();
                if (await _authRepository.UserExists(command.Email))
                {
                    await _bus.PublishEvent(new ExceptionEvent("user_already_exists", $"Email: {command.Email} is already in use", command));
                }

                var userToCreate = _map.Map<User>(command);

                var createdUser = await _authRepository.Register(userToCreate, command.Password);

                await _bus.PublishEvent(new UserCreatedEvent(createdUser.Email, command.Id));
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception", $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}
