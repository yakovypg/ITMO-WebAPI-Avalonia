using HarryPotter.Models;
using HarryPotter.Services;
using HarryPotter.ViewModels.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HarryPotter.ViewModels;

public class LikedCharactersViewModel()
    : LikeableEntitiesViewModel<Character>(HogwartsEntitiesService.LikedCharacters)
{
    public override async Task LoadEntitiesAsync()
    {
        await base.LoadEntitiesAsync();

        foreach (Character character in Entities)
        {
            character.IsLiked = true;
        }

        _ = LoadCharactersImages(Entities.ToArray());
    }

    protected override async Task LikeEntityAsync(Character entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        await UIActionExecutionService.Execute(
            LikedCharactersService.LikeCharacter(entity, Entities));
    }

    protected override async Task DislikeEntityAsync(Character entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        await UIActionExecutionService.Execute(
            LikedCharactersService.DislikeCharacter(entity, Entities));
    }
}
