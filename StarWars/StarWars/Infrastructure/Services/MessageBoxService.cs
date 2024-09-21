using Avalonia.Threading;
using MsBox.Avalonia;
using System;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Services
{
    public static class MessageBoxService
    {
        public static void ShowTaskError(Task task, string title = "Error")
        {
            ArgumentNullException.ThrowIfNull(task, nameof(task));

            title ??= string.Empty;
            string message = task.Exception?.Message ?? "Unknown error";

            ShowInUIThread(title, message);
        }

        public static void ShowInUIThread(string title, string message)
        {
            title ??= string.Empty;
            message ??= string.Empty;

            Dispatcher.UIThread.Post(async () =>
            {
                _ = await MessageBoxManager.GetMessageBoxStandard(title, message).ShowAsync();
            });
        }
    }
}
