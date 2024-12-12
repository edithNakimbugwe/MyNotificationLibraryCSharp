namespace NotificationScheduleLibrary;

public class LibraryApi(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, string senderEmail)
{
        private readonly ReminderScheduler _scheduler = new();
        private readonly NotificationService _notificationService = new(smtpServer, smtpPort, smtpUsername, smtpPassword, senderEmail);
        private readonly AdherenceLogger _adherenceLogger = new();

        public int ScheduleMedication(string medicationName, string dosage, DateTime time, string recipientName,string recipientEmail)
        {
            int reminderId = _scheduler.AddReminder(medicationName, dosage, time, recipientName, recipientEmail);
            _scheduler.GetReminders().Find(r => r.Id == reminderId)!.PatientEmail = recipientEmail;
            return reminderId;
        }

        public void TriggerDueReminders()
        {
            var dueReminders = _scheduler.GetDueReminders();
            foreach (var reminder in dueReminders.Where(reminder => !string.IsNullOrEmpty(reminder.PatientEmail)))
            {
                _notificationService.SendEmailNotification(reminder);
                LogAdherence(reminder.Id, AdherenceStatus.Taken);
            }
        }
        
        public void TriggerReminder(int reminderId)
        {
            var reminder = _scheduler.GetReminder(reminderId);
            _notificationService.SendEmailNotification(reminder);
            LogAdherence(reminder.Id, AdherenceStatus.Taken);
        }

        private void LogAdherence(int reminderId, AdherenceStatus status)
        {
            _adherenceLogger.LogAdherence(reminderId, status);
        }

        public void ShowLogs()
        {
            var logs = _adherenceLogger.GetAdherenceLogs();
            foreach (var log in logs)
            {
                Console.WriteLine($"Reminder ID: {log.ReminderId}, Status: {log.Status}, Timestamp: {log.Timestamp}");
            }
        }
    }