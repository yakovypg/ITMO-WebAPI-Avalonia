using HarryPotter.DataAccess;
using HarryPotter.Models;
using HarryPotter.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace HarryPotter.ViewModels.Base;

public abstract class LikedCharactersContainerViewModel : LikeableEntitiesViewModel<Character>
{
    private readonly LikedCharactersViewModel _likedEntities;

    public LikedCharactersContainerViewModel(
        LikedCharactersViewModel likedEntities,
        IHogwartsEntityRepository<Character> repository)
        : base(repository ?? throw new ArgumentNullException(nameof(repository)))
    {
        ArgumentNullException.ThrowIfNull(likedEntities, nameof(likedEntities));
        
        _likedEntities = likedEntities;
        _likedEntities.Entities.CollectionChanged += UpdateLikedEntities;
    }

    public override async Task LoadEntitiesAsync()
    {
        await base.LoadEntitiesAsync();

        if (!_likedEntities.IsEntitiesLoaded)
            await _likedEntities.LoadEntitiesAsync();

        UpdateLikedEntities();
        _ = LoadCharactersImages(Entities.ToArray());
    }

    protected override async Task LikeEntityAsync(Character entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        await UIActionExecutionService.Execute(
            LikedCharactersService.LikeCharacter(entity, _likedEntities.Entities));
    }

    protected override async Task DislikeEntityAsync(Character entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        await UIActionExecutionService.Execute(
            LikedCharactersService.DislikeCharacter(entity, _likedEntities.Entities));
    }

    private void UpdateLikedEntities()
    {
        foreach (Character character in Entities)
        {
            character.IsLiked = _likedEntities.Entities.Contains(character);
        }
    }

    private void UpdateLikedEntities(object? sender, NotifyCollectionChangedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e, nameof(e));

        IEnumerable<Character> oldItems = e.OldItems?.Cast<Character>()
            ?? Enumerable.Empty<Character>();

        IEnumerable<Character> newItems = e.NewItems?.Cast<Character>()
            ?? Enumerable.Empty<Character>();

        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                foreach (Character item in newItems)
                {
                    Character? character = Entities.FirstOrDefault(t => t.Equals(item));
                    
                    if (character is not null)
                        character.IsLiked = true;
                }
                break;

            case NotifyCollectionChangedAction.Remove:
                foreach (Character item in oldItems)
                {
                    Character? character = Entities.FirstOrDefault(t => t.Equals(item));
                    
                    if (character is not null)
                        character.IsLiked = false;
                }
                break;

            case NotifyCollectionChangedAction.Reset:
                foreach (Character entity in Entities)
                {
                    entity.IsLiked = false;
                }
                break;
        }
    }
}
