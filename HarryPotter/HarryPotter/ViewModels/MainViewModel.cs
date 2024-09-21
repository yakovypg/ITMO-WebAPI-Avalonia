using Avalonia.Controls;
using HarryPotter.Services;
using HarryPotter.ViewModels.Base;
using ReactiveUI;
using System;
using System.Reactive;

namespace HarryPotter.ViewModels;

public class MainViewModel : ViewModelBase
{
    private int _selectedPageIndex;
    private string? _selectedPageTitle = "Liked Characters";

    public MainViewModel()
    {
        SpellsViewModel = new();
        LikedCharactersViewModel = new();
        CharactersInHouseViewModel = new(LikedCharactersViewModel);
        CharactersViewModel = new(LikedCharactersViewModel);
        HogwartsStaffViewModel = new(LikedCharactersViewModel);
        HogwartsStudentsViewModel = new(LikedCharactersViewModel);

        ShowPreviousPageCommand = ReactiveCommand.Create<int>(ShowPreviousPage);
        ShowNextPageCommand = ReactiveCommand.Create<int>(ShowNextPage);
        ChangePageTitleCommand = ReactiveCommand.Create<SelectionChangedEventArgs>(ChangePageTitle);
    }

    public int SelectedPageIndex
    {
        get => _selectedPageIndex;
        private set => this.RaiseAndSetIfChanged(ref _selectedPageIndex, value);
    }

    public string? SelectedPageTitle
    {
        get => _selectedPageTitle;
        private set => this.RaiseAndSetIfChanged(ref _selectedPageTitle, value);
    }

    public SpellsViewModel SpellsViewModel { get; }
    public LikedCharactersViewModel LikedCharactersViewModel { get; }
    public CharactersInHouseViewModel CharactersInHouseViewModel { get; }
    public CharactersViewModel CharactersViewModel { get; }
    public HogwartsStaffViewModel HogwartsStaffViewModel { get; }
    public HogwartsStudentsViewModel HogwartsStudentsViewModel { get; }

    public ReactiveCommand<int, Unit> ShowPreviousPageCommand { get; }
    public ReactiveCommand<int, Unit> ShowNextPageCommand { get; }
    public ReactiveCommand<SelectionChangedEventArgs, Unit> ChangePageTitleCommand { get; }

    private void ShowPreviousPage(int pagesCount)
    {
        SelectedPageIndex = SelectedPageIndex == 0
            ? (pagesCount - 1)
            : (SelectedPageIndex - 1);
    }

    private void ShowNextPage(int pagesCount)
    {
        SelectedPageIndex = SelectedPageIndex == (pagesCount - 1)
            ? 0
            : (SelectedPageIndex + 1);
    }

    public void ChangePageTitle(SelectionChangedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e, nameof(e));

        object? page = e.AddedItems.Count > 0
            ? e.AddedItems[0]
            : null;

        SelectedPageTitle = PageTitleService.GetTitle(page);
    }
}
