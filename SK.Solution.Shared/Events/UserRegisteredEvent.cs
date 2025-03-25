using MediatR;

namespace SK.Solution.Shared.Events
{
    public record UserRegisteredEvent(string UserId, string Email) : INotification;
}
