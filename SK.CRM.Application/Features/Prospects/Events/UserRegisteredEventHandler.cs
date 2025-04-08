using MediatR;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.Solution.Shared.Events;

namespace SK.CRM.Application.Features.Prospects.Events
{
    public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        private readonly ICustomerRepository _customerRepository;

        public UserRegisteredEventHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var customer = new Customer(notification.UserId, notification.Email, notification.firstName, notification.lastName);
            
            customer = await _customerRepository.CreateAsync(customer);
        }
    }
}
