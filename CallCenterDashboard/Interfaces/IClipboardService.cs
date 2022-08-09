namespace CallingDashboard.Interfaces;

public interface IClipboardService
{
    Task CopyToClipboard(string text);
}