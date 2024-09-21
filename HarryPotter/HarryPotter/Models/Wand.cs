using System;

namespace HarryPotter.Models;

public class Wand
{
    public string? Wood { get; set; }
    public string? Core { get; set; }
    public double? Length { get; set; }

    public bool Equals(Wand? other)
    {
        return other is not null
            && Wood == other.Wood
            && Core == other.Core
            && Length == other.Length;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Wand);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Wood, Core, Length);
    }

    public override string ToString()
    {
        return $"{nameof(Wood)}: {Wood}, " +
               $"{nameof(Core)}: {Core}, " +
               $"{nameof(Length)}: {Length}";
    }
}
