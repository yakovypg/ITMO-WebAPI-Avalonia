using IPManager.Infrastructure.Services;
using IPManager.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace IPManager.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private string _ipInfo = string.Empty;

    private ItemPosition<IpAddress>? _selectedIp;
    private readonly Action _showSearchPage;

    public HomeViewModel() : this(() => { }, [])
    {
    }

    public HomeViewModel(
        Action showSearchPage,
        ObservableCollection<ItemPosition<IpAddress>> favoriteIps)
    {
        ArgumentNullException.ThrowIfNull(showSearchPage, nameof(showSearchPage));
        ArgumentNullException.ThrowIfNull(favoriteIps, nameof(favoriteIps));

        _showSearchPage = showSearchPage;
        FavoriteIps = favoriteIps;

        ShowSearchPageCommand = ReactiveCommand.Create(_showSearchPage);

        _ = this.WhenAnyValue(vm => vm.SelectedIp)
            .Subscribe(t => IpInfo = t?.Item.Details ?? string.Empty);
    }

    public static string UserIp => LocalIpService.GetLocalIPAddress();

    public string IpInfo
    {
        get => _ipInfo;
        set => this.RaiseAndSetIfChanged(ref _ipInfo, value);
    }

    public ItemPosition<IpAddress>? SelectedIp
    {
        get => _selectedIp;
        set => this.RaiseAndSetIfChanged(ref _selectedIp, value);
    }

    public ObservableCollection<ItemPosition<IpAddress>> FavoriteIps { get; }
    public ReactiveCommand<Unit, Unit> ShowSearchPageCommand { get; }
}
