using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions;
using Pomogotchi.API.Extensions.Notifications;

namespace Pomogotchi.Tests.Mocks
{
    public class NotificationHistoryExtension : IAPIExtension
    {
        public List<(DateTime, GenericNotification)> History { get; protected set; } = new();

        public CommandResult Notify(GenericNotification notification)
        {
            History.Add((DateTime.Now, notification));
            return CommandResult.Success();
        }
    }

    public static class NotificationHistoryExtensionMethods{
            public static NotificationHistoryExtension AddNotificationHistory(this ApiControllerBase controller){
                var extension = new NotificationHistoryExtension();
                controller.AddExtension(extension);

                return extension;
            }
        }
}