using FluentValidation.Results;


namespace Ingenius.Services
{
    public static class Notificator
    {

        public static MessageNotification Notification(ValidationResult validationResult)
        {
            string messagens = string.Empty;
            foreach (var error in validationResult.Errors)
            {
                messagens += $"{error.ErrorMessage} \n";
            }
           return new MessageNotification(messagens, "Erro", MessageBoxIcon.Error);
        }

        public static MessageNotification Notification(string messagem, string titulo, MessageBoxIcon icone)
        {
            return new MessageNotification(messagem, "Erro", MessageBoxIcon.Information);
        }

    }
}
