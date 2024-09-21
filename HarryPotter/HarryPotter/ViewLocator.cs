using Avalonia.Controls;
using Avalonia.Controls.Templates;
using HarryPotter.ViewModels.Base;
using System;

namespace HarryPotter;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        string? fullName = data.GetType().FullName;

        string? name = fullName?.Replace("ViewModel", "View", StringComparison.Ordinal)
            ?? string.Empty;

        Type? type = Type.GetType(name);

        if (type is not null)
            return (Control)Activator.CreateInstance(type)!;

        return new TextBlock
        {
            Text = $"Not Found: {name}"
        };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}