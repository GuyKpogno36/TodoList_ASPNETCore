using Microsoft.EntityFrameworkCore;
using Todo_List_ASPNETCore.Controllers;
using Todo_List_ASPNETCore.DAL;

namespace Todo_List_ASPNETCore.Services
{
    public class NotificationBackgroundService: BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EmailService _emailService;

        public NotificationBackgroundService(IServiceProvider serviceProvider, EmailService emailService)
        {
            _serviceProvider = serviceProvider;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var notificationController = scope.ServiceProvider.GetRequiredService<NotificationController>();
                    await notificationController.SendNotifications(null);
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Vérifie toutes les heures
            }
        }

        public async Task SendNotificationsAsync(string email, string subject, string message)
        {
            _emailService.SendEmail(email, subject, message);
        }
    }
}
