namespace TodoApp.API.Notifications
{
    public class DomainNotifier
    {
        private readonly List<Notification> _notifications = [];

        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

        public bool HasNotifications => _notifications.Count != 0;

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void Clear() => _notifications.Clear();

    }
}
