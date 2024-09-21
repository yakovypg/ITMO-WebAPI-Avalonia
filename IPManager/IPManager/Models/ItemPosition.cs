namespace IPManager.Models;

public class ItemPosition<T>(T item, int position)
{
    public T Item { get; init; } = item;
    public int Position { get; init; } = position;

    public override string ToString()
    {
        return $"{Position}. {Item}";
    }
}
