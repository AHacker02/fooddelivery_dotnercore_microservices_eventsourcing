using System;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Events;
using OMF.CustomerManagementService.Command.Repository.Abstractions;
using OMF.CustomerManagementService.Command.Repository.DataContext;
using OMF.CustomerManagementService.Command.Service.Command;
using OMF.CustomerManagementService.Command.Service.Event;
using ServiceBus.Abstractions;

namespace OMF.CustomerManagementService.Command.Service.CommandHandlers
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

        /// <summary>
        /// Create user command handler
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task HandleAsync(CreateUserCommand command)
        {
            try
            {
                command.Email = command.Email.ToLower();
                var userToCreate = _map.Map<TblCustomer>(command);

                if (await _authRepository.UserExists(command.Email))
                    await _bus.PublishEvent(new ExceptionEvent("user_already_exists",
                        $"Email: {command.Email} is already in use", command));

                var createdUser = await _authRepository.Register(userToCreate, command.Password);

                await _bus.PublishEvent(new UserCreatedEvent(createdUser.Email, command.Id));
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}