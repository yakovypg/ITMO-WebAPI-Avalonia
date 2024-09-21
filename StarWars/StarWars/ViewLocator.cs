using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using StarWars.ViewModels;

namespace StarWars;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        string? name = data.GetType().FullName?.Replace("ViewModel", "View", StringComparison.Ordinal);
        Type? type = Type.GetType(name ?? string.Empty);

        return type is not null
            ? (Control)Activator.CreateInstance(type)!
            : new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}