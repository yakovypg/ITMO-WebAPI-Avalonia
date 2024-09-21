using System;
using System.Threading.Tasks;

namespace HarryPotter.Services;

public static class UIActionExecutionService
{
    private const string _defaultErrorMessage = "Failed to perform action";

    public static async Task Execute(Task action, string? errorMessage = _defaultErrorMessage)
    {
        ArgumentNullException.ThrowIfNull(action, nameof(action));

        try
        {
            await action;
        }
        catch (Exception ex)
        {
            string message = $"{errorMessage}. {ex.Message}";
            UIMessageService.ShowMessage("Error", message);
        }
    }
}
