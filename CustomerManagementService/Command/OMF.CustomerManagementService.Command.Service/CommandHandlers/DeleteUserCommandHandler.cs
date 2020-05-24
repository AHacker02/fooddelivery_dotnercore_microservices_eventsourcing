using System;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Events;
using OMF.CustomerManagementService.Command.Repository.Abstractions;
using OMF.CustomerManagementService.Command.Repository.DataContext;
using OMF.CustomerManagementService.Command.Service.Command;
using ServiceBus.Abstractions;

namespace OMF.CustomerManagementService.Command.Service.CommandHandlers
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

        /// <summary>
        /// Deactivate user command handler
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task HandleAsync(DeleteUserCommand command)
        {
            try
            {
                command.Email = command.Email.ToLower();
                if (!await _authRepository.UserExists(command.Email))
                    await _bus.PublishEvent(new ExceptionEvent("user_doesn't_exists",
                        $"Email: {command.Email} is not present in the system", command));

                var userToDelete = _map.Map<TblCustomer>(command);

                await _authRepository.DeleteUser(userToDelete, command.Password);
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}