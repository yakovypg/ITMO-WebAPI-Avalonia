using HarryPotter.DataAccess;
using HarryPotter.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarryPotter.ViewModels.Base
{
    public abstract class LikeableEntitiesViewModel<T> : EntitiesViewModel<T>
        where T : HogwartsEntity
    {
        protected LikeableEntitiesViewModel(IHogwartsEntityRepository<T> repository)
            : base(repository ?? throw new ArgumentNullException(nameof(repository)))
        {
            SwitchEntityLikeCommand = ReactiveCommand.Create<T, Task>(SwitchEntityLikeAsync);
        }

        public ReactiveCommand<T, Task> SwitchEntityLikeCommand { get; }

        protected virtual async Task SwitchEntityLikeAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            if (entity.IsLiked)
                await DislikeEntityAsync(entity);
            else
                await LikeEntityAsync(entity);
        }

        protected virtual async Task LoadCharactersImages(IEnumerable<Character> characters)
        {
            ArgumentNullException.ThrowIfNull(characters, nameof(characters));

            foreach (Character character in characters)
            {
                await character.LoadBitmapImageAsync();
            }
        }

        protected abstract Task LikeEntityAsync(T entity);
        protected abstract Task DislikeEntityAsync(T entity);
    }
}
