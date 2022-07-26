using MudBlazor;

namespace CallCenterDashboard.Models;

public record NotificationData(string Message, string ActionName, Severity Severity);