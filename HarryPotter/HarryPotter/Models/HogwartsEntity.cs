using Newtonsoft.Json;
using System;

namespace HarryPotter.Models;

public abstract class HogwartsEntity : ReactiveModel, IEquatable<HogwartsEntity>
{
    private bool _isLiked;

    [JsonIgnore]
    public bool IsLiked
    {
        get => _isLiked;
        set => RaiseAndSetIfChanged(ref _isLiked, value);
    }

    public string? Id { get; set; }

    public bool Equals(HogwartsEntity? other)
    {
        return other is not null && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as HogwartsEntity);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
