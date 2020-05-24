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
    public class UpdateUserCommandHandler:ICommandHandler<UpdateUserCommand>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEventBus _bus;
        private readonly IMapper _map;

        public UpdateUserCommandHandler(IAuthRepository authRepository, IEventBus bus, IMapper map)
        {
            _authRepository = authRepository;
            _bus = bus;
            _map = map;
        }

        public async Task HandleAsync(UpdateUserCommand command)
        {
            try
            {
                var updatedUser = _map.Map<TblCustomer>(command);
                updatedUser.ModifiedDate = DateTime.UtcNow;
                updatedUser.Active=true;
                await _authRepository.UpdateUser(updatedUser, command.Password);
            }
            catch (Exception ex)
            {
                await _bus.PublishEvent(new ExceptionEvent("system_exception",
                    $"Message: {ex.Message} Stacktrace: {ex.StackTrace}", command));
            }
        }
    }
}