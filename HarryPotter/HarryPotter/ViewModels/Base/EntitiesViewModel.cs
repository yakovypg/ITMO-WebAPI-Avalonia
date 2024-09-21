using HarryPotter.DataAccess;
using HarryPotter.Models;
using HarryPotter.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace HarryPotter.ViewModels.Base;

public abstract class EntitiesViewModel<T> : ViewModelBase
    where T : HogwartsEntity
{
    private bool _isEntitiesLoaded;
    private bool _isEntitiesLoading;

    private readonly IHogwartsEntityRepository<T> _repository;

    protected EntitiesViewModel(IHogwartsEntityRepository<T> repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;

        Entities = [];
        CheckIfEntitiesLoadedBeforeLoading = true;

        LoadEntitiesCommand = ReactiveCommand.Create(StartLoadingEntities);
    }

    public bool IsEntitiesLoaded
    {
        get => _isEntitiesLoaded;
        private set => this.RaiseAndSetIfChanged(ref _isEntitiesLoaded, value);
    }

    public bool IsEntitiesLoading
    {
        get => _isEntitiesLoading;
        protected set
        {
            this.RaiseAndSetIfChanged(ref _isEntitiesLoading, value);

            if (value)
                IsEntitiesLoaded = false;
        }
    }

    public ObservableCollection<T> Entities { get; }
    public ReactiveCommand<Unit, Task> LoadEntitiesCommand { get; }

    protected bool CheckIfEntitiesLoadedBeforeLoading { get; set; }

    public virtual Task StartLoadingEntities()
    {
        return LoadEntitiesAsync();
    }

    public virtual async Task LoadEntitiesAsync()
    {
        if (CheckIfEntitiesLoadedBeforeLoading && IsEntitiesLoaded)
            return;

        IsEntitiesLoading = true;

        try
        {
            IAsyncEnumerable<T> entities = _repository.FindAllAsync();

            Entities.Clear();

            await foreach (T entity in entities)
            {
                Entities.Add(entity);
            }

            IsEntitiesLoaded = true;
        }
        catch (Exception ex)
        {
            string message = $"Failed to load entities. {ex.Message}";
            UIMessageService.ShowMessage("Error", message);
        }
        finally
        {
            IsEntitiesLoading = false;
        }
    }
}
