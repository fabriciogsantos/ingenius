namespace Ingenius.Services
{
    public class MessageNotification
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public MessageBoxIcon MessageBoxIcon { get; set; }
        public MessageNotification(string message, string title, MessageBoxIcon messageBoxIcon)
        {
            Message = message;
            Title = title;
            MessageBoxIcon = messageBoxIcon;
        }
       
    }

    public enum MessageBoxIcon
    {
        Error = 16,
        Information = 64
    }
}
