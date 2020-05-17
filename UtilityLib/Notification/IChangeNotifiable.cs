using System;

namespace Libraries.UtilityLib.Notification
{
    public interface IChangeNotifiable
    {
        event ChangeNotificationHandler OnChangeNotification;
    }

    public delegate void ChangeNotificationHandler(IChangeNotifiable sender, ChangeNotificationEventArgs args);

    public class ChangeNotificationEventArgs : EventArgs
    {
    }
}

