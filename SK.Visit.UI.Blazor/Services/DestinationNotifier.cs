namespace SK.Visit.UI.Blazor.Services
{
    public class DestinationNotifier
    {
        public event Action? OnDestinationsChanged;

        public void NotifyDestinationsChanged()
        {
            OnDestinationsChanged?.Invoke();
        }
    }

}
