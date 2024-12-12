namespace NotificationScheduleLibrary;

public class ReminderScheduler
{
    private readonly List<Reminder> _reminders = new List<Reminder>(); //Tentative Database for all our reminders - Could be a service like SQLite etc

    public int AddReminder(string medicationName, string dosage, DateTime time, string name, string email)
    {
        _reminders.Add(new Reminder { MedicationName = medicationName, Dosage = dosage, ReminderTime = time, PatientName = name, PatientEmail = email });
        var addedReminder = _reminders.Find(x => x.MedicationName == medicationName && x.Dosage == dosage && x.ReminderTime == time && x.PatientName == name && x.PatientEmail == email);
        return addedReminder.Id;
    }

    public void UpdateReminder(int reminderId, DateTime newTime)
    {
        var reminder = _reminders.Find(r => r.Id == reminderId);
        if (reminder != null)
        {
            reminder.ReminderTime = newTime;
        }
    }
        
    public Reminder GetReminder(int reminderId)
    {
        DateTime currentTime = DateTime.Now;
        var reminder = _reminders.Find(r => r.Id == reminderId && r.ReminderTime <= currentTime);
        return reminder;
    }

    public void RemoveReminder(int reminderId)
    {
        _reminders.RemoveAll(r => r.Id == reminderId);
    }
        
    public List<Reminder> GetDueReminders()
    {
        DateTime currentTime = DateTime.Now;
        return _reminders.FindAll(r => r.ReminderTime <= currentTime);
    }


    public List<Reminder> GetReminders()
    {
        return _reminders;
    }
}

public class Reminder
{
    public int Id { get; set; }
    public string MedicationName { get; set; }
    public string Dosage { get; set; }
    public DateTime ReminderTime { get; set; }
    public string PatientName { get; set; }
    public string PatientEmail { get; set; }
}