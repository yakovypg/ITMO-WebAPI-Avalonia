using IPManager.Infrastructure.Services;
using IPManager.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IPManager.ViewModels;

public class MainViewModel : ViewModelBase
{
    private bool _isHomePageVisible = false;
    private bool _isSearchPageVisible = true;
    private bool _isFavoriteIpsLoaded = false;

    private readonly ObservableCollection<ItemPosition<IpAddress>> _favoriteIps;

    public MainViewModel()
    {
        _favoriteIps = [];

        HomeViewModel = new HomeViewModel(ShowSearchPage, _favoriteIps);
        SearchViewModel = new SearchViewModel(async () => await ShowHomePageAsync(), _favoriteIps);
    }

    public HomeViewModel HomeViewModel { get; }
    public SearchViewModel SearchViewModel { get; }

    public bool IsHomePageVisible
    {
        get => _isHomePageVisible;
        private set => this.RaiseAndSetIfChanged(ref _isHomePageVisible, value);
    }

    public bool IsSearchPageVisible
    {
        get => _isSearchPageVisible;
        private set => this.RaiseAndSetIfChanged(ref _isSearchPageVisible, value);
    }

    private void HideAllPages()
    {
        IsHomePageVisible = false;
        IsSearchPageVisible = false;
    }

    private async Task ShowHomePageAsync()
    {
        await LoadFavoriteIpsAsync();

        HideAllPages();
        IsHomePageVisible = true;
    }

    private void ShowSearchPage()
    {
        HideAllPages();
        IsSearchPageVisible = true;
    }

    private async Task LoadFavoriteIpsAsync()
    {
        try
        {
            var ips = FavoriteIpService.GetFavoriteIpAddresses();
            var items = ips.Select((t, ind) => new ItemPosition<IpAddress>(t, ind + 1));

            await foreach (var item in items)
            {
                if (!_favoriteIps.Any(t => t.Item.Ip == item.Item.Ip))
                    _favoriteIps.Add(item);
            }
        }
        catch (Exception ex)
        {
            string message = $"Failed to load favorite IPs. Reason: {ex.Message}";
            PopupService.ShowErrorInUIThread(message);
        }
        finally
        {
            _isFavoriteIpsLoaded = true;
        }
    }
}
