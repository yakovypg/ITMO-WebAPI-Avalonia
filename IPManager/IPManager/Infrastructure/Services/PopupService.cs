using Avalonia.Threading;
using MsBox.Avalonia;

namespace IPManager.Infrastructure.Services;

public static class PopupService
{
    public static void ShowMessageInUIThread(string title, string message)
    {
        title ??= string.Empty;
        message ??= string.Empty;

        Dispatcher.UIThread.Post(async () =>
        {
            _ = await MessageBoxManager
                .GetMessageBoxStandard("Error", message)
                .ShowAsync();
        });
    }

    public static void ShowErrorInUIThread(string message)
    {
        ShowMessageInUIThread("Error", message);
    }

    public static void ShowInfoInUIThread(string message)
    {
        ShowMessageInUIThread("Info", message);
    }
}
