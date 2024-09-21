using Avalonia.Threading;
using MsBox.Avalonia;

namespace HarryPotter.Services;

public static class UIMessageService
{
    public static void ShowMessage(string title, string message)
    {
        Dispatcher.UIThread.Post(async () =>
        {
            _ = await MessageBoxManager
                .GetMessageBoxStandard(title ?? string.Empty, message ?? string.Empty)
                .ShowAsync();
        });
    }
}
