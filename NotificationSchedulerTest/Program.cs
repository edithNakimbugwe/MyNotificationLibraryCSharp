using NotificationScheduleLibrary;

const string smtpHost = "smtp.gmail.com";
const int smtpPort = 587;
const string senderEmail = "shanitadith@gmail.com";
const string senderPassword = "atgg wvuk thpk bdcs";

var api = new LibraryApi(
    smtpServer: smtpHost, 
    smtpPort: smtpPort,                              
    smtpUsername: senderEmail,      
    smtpPassword: senderPassword,              
    senderEmail: senderEmail   
);
            
var reminderId = api.ScheduleMedication("Paracetamol", "100mg", DateTime.Now.AddSeconds(5), "Elvis","mwebyaelvis9@gmail.com");
            
Thread.Sleep(5000);
            
api.TriggerReminder(reminderId);
            
api.ShowLogs();