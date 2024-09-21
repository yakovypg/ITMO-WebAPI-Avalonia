﻿using HarryPotter.Services;
using HarryPotter.ViewModels.Base;
using System;

namespace HarryPotter.ViewModels;

public class CharactersInHouseViewModel(LikedCharactersViewModel likedEntities)
    : LikedCharactersContainerViewModel(
        likedEntities ?? throw new ArgumentNullException(nameof(likedEntities)),
        HogwartsEntitiesService.CharactersInHouse)
{
}
