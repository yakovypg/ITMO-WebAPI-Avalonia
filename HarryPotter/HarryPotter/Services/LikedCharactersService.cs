using HarryPotter.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarryPotter.Services;

public static class LikedCharactersService
{
    public static async Task LikeCharacter(
        Character character,
        ICollection<Character> likedCharacters)
    {
        ArgumentNullException.ThrowIfNull(character, nameof(character));
        ArgumentNullException.ThrowIfNull(likedCharacters, nameof(likedCharacters));

        if (likedCharacters.Contains(character))
            return;

        await HogwartsEntitiesService.LikedCharactersHandler.AddAsync(character);

        likedCharacters.Add(character);
        character.IsLiked = true;
    }

    public static async Task DislikeCharacter(
        Character character,
        ICollection<Character> likedCharacters)
    {
        ArgumentNullException.ThrowIfNull(character, nameof(character));
        ArgumentNullException.ThrowIfNull(likedCharacters, nameof(likedCharacters));

        if (!likedCharacters.Contains(character))
            return;

        await HogwartsEntitiesService.LikedCharactersHandler.DeleteAsync(character);

        if (likedCharacters.Remove(character))
            character.IsLiked = false;
    }
}
