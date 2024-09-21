using ReactiveUI;
using StarWars.Infrastructure.Extensions;
using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace StarWars.ViewModels
{
    public class FavoriteEntitiesViewModel : ViewModelBase
    {
        private bool _isDataLoading;
        private bool _isFavoriteEntitiesLoaded;

        public FavoriteEntitiesViewModel()
        {
            Entities = [];

            LoadFavoriteEntitiesCommand = ReactiveCommand.Create(LoadFavoriteEntitiesAsync);
            LoadLazyEntityDataCommand = ReactiveCommand.Create<Entity, Task<bool>>(LoadLazyEntityDataAsync);
            SwitchBelongingToFavoriteEntitiesCommand = ReactiveCommand.Create<Entity, Task<bool>>(SwitchBelongingToFavoriteEntities);
        }

        public bool IsDataLoading
        {
            get => _isDataLoading;
            protected set => this.RaiseAndSetIfChanged(ref _isDataLoading, value);
        }

        public bool IsFavoriteEntitiesLoaded
        {
            get => _isDataLoading;
            protected set => this.RaiseAndSetIfChanged(ref _isFavoriteEntitiesLoaded, value);
        }

        public ObservableCollection<Entity> Entities { get; }

        public ReactiveCommand<Unit, Task> LoadFavoriteEntitiesCommand { get; }
        public ReactiveCommand<Entity, Task<bool>> LoadLazyEntityDataCommand { get; }
        public ReactiveCommand<Entity, Task<bool>> SwitchBelongingToFavoriteEntitiesCommand { get; }

        public virtual async Task LoadFavoriteEntitiesAsync()
        {
            IsDataLoading = true;
            List<Entity> favoriteEntities = [];

            try
            {
                IAsyncEnumerable<Entity> entities = EntityDataService.FavoriteEntityRepository.FindAllEntitiesAsync();

                await foreach (Entity entity in entities)
                {
                    favoriteEntities.Add(entity);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBoxService.ShowInUIThread("Error", "Failed to load favorite entities");
                return;
            }

            Entities.Reset(favoriteEntities);

            IsDataLoading = false;
            IsFavoriteEntitiesLoaded = true;
        }

        protected virtual async Task<bool> AddFavoriteEntity(Entity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            IsDataLoading = true;

            if (Entities.Contains(entity))
            {
                IsDataLoading = false;
                return false;
            }

            Task task = EntityDataService.FavoriteEntityRepository.AddAsync(entity);
            await task;

            if (!task.IsCompletedSuccessfully)
            {
                MessageBoxService.ShowInUIThread("Error", "Failed to add favorite entity");

                IsDataLoading = false;
                return false;
            }

            Entity? entityWithSameType = Entities.FirstOrDefault(t => t.GetType() == entity.GetType());

            if (entityWithSameType is null)
            {
                Entities.Add(entity);
            }
            else
            {
                int index = Entities.IndexOf(entityWithSameType);
                Entities.Insert(index, entity);
            }

            IsDataLoading = false;
            return true;
        }

        protected virtual async Task<bool> RemoveFavoriteEntity(Entity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            IsDataLoading = true;

            if (!Entities.Contains(entity))
            {
                IsDataLoading = false;
                return false;
            }

            Task task = EntityDataService.FavoriteEntityRepository.RemoveAsync(entity);
            await task;

            if (!task.IsCompletedSuccessfully)
            {
                MessageBoxService.ShowInUIThread("Error", "Failed to remove favorite entity");

                IsDataLoading = false;
                return false;
            }

            _ = Entities.Remove(entity);

            IsDataLoading = false;
            return true;
        }

        protected virtual async Task<bool> LoadLazyEntityDataAsync(Entity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            Task task = LazyEntityDataService.LoadAllData(entity);
            await task;

            if (!task.IsCompletedSuccessfully)
            {
                MessageBoxService.ShowInUIThread("Error", "Failed to convert and show entity data");
                return false;
            }

            return true;
        }

        private async Task<bool> SwitchBelongingToFavoriteEntities(Entity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            entity.IsFavorite = !entity.IsFavorite;

            return entity.IsFavorite
                ? await AddFavoriteEntity(entity)
                : await RemoveFavoriteEntity(entity);
        }
    }
}
