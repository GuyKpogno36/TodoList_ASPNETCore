using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_List_ASPNETCore.DAL;
using Todo_List_ASPNETCore.Services;

namespace Todo_List_ASPNETCore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Notification : ControllerBase
    {
        private readonly NotificationBackgroundService _notificationService;
        private readonly TodoListContext _context;
        private readonly EmailService _emailService;

        public Notification(NotificationBackgroundService notificationService, TodoListContext context, EmailService emailService)
        {
            _notificationService = notificationService;
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("send-notifications")]
        public async Task<IActionResult> SendNotifications([FromBody] string UserEmail)
        {
            var upcomingTasks = await _context.TASK
                .Where(t => t.Task_Deadline > DateTime.Now && t.Task_Deadline <= DateTime.Now.AddDays(1) && !t.Task_Status).ToListAsync();

            var overdueTasks = await _context.TASK
                .Where(t => t.Task_Deadline < DateTime.Now && !t.Task_Status).ToListAsync();

            string subject = "", message = "";

            foreach (var task in upcomingTasks)
            {
                message = $"Reminder: Your task '{task.Task_Title}' is due on {task.Task_Deadline}.";
                subject = "Upcoming Task Reminder";
            }

            foreach (var task in overdueTasks)
            {
                message = $"Reminder: Your task '{task.Task_Title}' is overdue since {task.Task_Deadline}.";
                subject = "Overdue Task Reminder";
            }
            await _notificationService.SendNotificationsAsync(UserEmail, subject, message);
            return Ok("Notifications sent.");
        }
    }
}
