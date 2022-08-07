using MudBlazor;

namespace CallingDashboard.Models;

public record NotificationData(string Message, string ActionName, Severity Severity);