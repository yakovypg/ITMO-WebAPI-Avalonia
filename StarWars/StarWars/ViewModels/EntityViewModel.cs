using Avalonia.Media;
using DynamicData;
using ReactiveUI;
using StarWars.DataAccess;
using StarWars.Infrastructure.Converters;
using StarWars.Infrastructure.Extensions;
using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace StarWars.ViewModels
{
    public abstract class EntityViewModel<TEntity, TEntityData> : ViewModelBase
        where TEntity : Entity
        where TEntityData : EntityBaseData
    {
        private string _loadCurrentPageButtonContent = "Load";
        private int _currentPage = 1;
        private bool _hasPreviousPage;
        private bool _hasNextPage;
        private bool _isDataLoading;

        public EntityViewModel()
            : this(new FavoriteEntitiesViewModel())
        {
        }

        public EntityViewModel(FavoriteEntitiesViewModel favoriteEntitiesViewModel)
        {
            FavoriteEntitiesViewModel = favoriteEntitiesViewModel
                ?? throw new ArgumentNullException(nameof(favoriteEntitiesViewModel));

            Entities = [];

            ShowPreviousPageCommand = ReactiveCommand.Create(ShowPreviousPageAsync);
            ShowNextPageCommand = ReactiveCommand.Create(ShowNextPageAsync);
            LoadCurrentPageCommand = ReactiveCommand.Create(LoadCurrentPageAsync);
            LoadLazyEntityDataCommand = ReactiveCommand.Create<TEntity, Task<bool>>(LoadLazyEntityDataAsync);
        }

        public FavoriteEntitiesViewModel FavoriteEntitiesViewModel { get; }

        public string LoadCurrentPageButtonContent
        {
            get => _loadCurrentPageButtonContent;
            private set => this.RaiseAndSetIfChanged(ref _loadCurrentPageButtonContent, value);
        }

        public int CurrentPage
        {
            get => _currentPage;
            protected set => this.RaiseAndSetIfChanged(ref _currentPage, value);
        }

        public bool HasPreviousPage
        {
            get => _hasPreviousPage;
            protected set => this.RaiseAndSetIfChanged(ref _hasPreviousPage, value);
        }

        public bool HasNextPage
        {
            get => _hasNextPage;
            protected set => this.RaiseAndSetIfChanged(ref _hasNextPage, value);
        }

        public bool IsDataLoading
        {
            get => _isDataLoading;
            protected set
            {
                _ = this.RaiseAndSetIfChanged(ref _isDataLoading, value);

                if (value)
                    LoadCurrentPageButtonContent = "Reload";
            }
        }

        public ObservableCollection<TEntity> Entities { get; }

        public ReactiveCommand<Unit, Task> ShowPreviousPageCommand { get; }
        public ReactiveCommand<Unit, Task> ShowNextPageCommand { get; }
        public ReactiveCommand<Unit, Task> LoadCurrentPageCommand { get; }
        public ReactiveCommand<TEntity, Task<bool>> LoadLazyEntityDataCommand { get; }

        protected abstract IEntityDataRepository<TEntityData> EntityRepository { get; }
        protected abstract IEntityDataConverter<TEntityData, TEntity> EntityConverter { get; }

        protected virtual async Task ShowPreviousPageAsync()
        {
            if (!HasPreviousPage)
                return;

            CurrentPage--;
            await LoadCurrentPageAsync();
        }

        protected virtual async Task ShowNextPageAsync()
        {
            if (!HasNextPage)
                return;

            CurrentPage++;
            await LoadCurrentPageAsync();
        }

        protected virtual async Task LoadCurrentPageAsync()
        {
            IsDataLoading = true;
            EntityPage<TEntityData> page = await EntityRepository.FindEntitiesAsync(CurrentPage);

            IEnumerable<TEntityData> entitiesData = page.Results;
            IEnumerable<TEntity> entities;

            try
            {
                entities = entitiesData.Select(EntityConverter.Convert).ToArray();
            }
            catch
            {
                MessageBoxService.ShowInUIThread("Error", "Failed to convert and show loaded entities");
                IsDataLoading = false;
                return;
            }

            await SyncBelongingToFavoriteEntities(entities);
            Entities.Reset(entities);

            HasPreviousPage = !string.IsNullOrEmpty(page.Previous);
            HasNextPage = !string.IsNullOrEmpty(page.Next);
            IsDataLoading = false;
        }

        protected virtual async Task<bool> LoadLazyEntityDataAsync(TEntity entity)
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

        private async Task SyncBelongingToFavoriteEntities(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities, nameof(entities));

            if (!FavoriteEntitiesViewModel.IsFavoriteEntitiesLoaded)
                await FavoriteEntitiesViewModel.LoadFavoriteEntitiesAsync();

            foreach (Entity favoriteEntity in FavoriteEntitiesViewModel.Entities)
            {
                Entity? entity = entities.FirstOrDefault(t => t.Url == favoriteEntity.Url);

                if (entity is not null)
                    entity.IsFavorite = favoriteEntity.IsFavorite;
            }
        }
    }
}
