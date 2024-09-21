using IPManager.Infrastructure.Services;
using IPManager.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Threading.Tasks;

namespace IPManager.ViewModels;

public class SearchViewModel : ViewModelBase
{
    private const int _max_organizations_count = 10;

    private string _ipInfo = string.Empty;
    private string _currentIp = string.Empty;

    private Organization? _selectedOrganization;
    private readonly Action _showHomePage;

    public SearchViewModel() : this(() => { }, [])
    {
    }

    public SearchViewModel(
        Action showHomePage,
        ObservableCollection<ItemPosition<IpAddress>> favoriteIps)
    {
        ArgumentNullException.ThrowIfNull(showHomePage, nameof(showHomePage));
        ArgumentNullException.ThrowIfNull(favoriteIps, nameof(favoriteIps));

        _showHomePage = showHomePage;

        FavoriteIps = favoriteIps;
        Organizations = [];

        ShowHomePageCommand = ReactiveCommand.Create(_showHomePage);
        AddIpToFavoritesCommand = ReactiveCommand.Create<string?, Task>(AddIpToFavoritesAsync);
        FindEntityCommand = ReactiveCommand.Create<string?, Task>(FindEntityAsync);

        _ = this.WhenAnyValue(vm => vm.SelectedOrganization)
            .Subscribe(async t => await FindIpAsync(t?.Ip));
    }

    public string IpInfo
    {
        get => _ipInfo;
        set => this.RaiseAndSetIfChanged(ref _ipInfo, value);
    }

    public string CurrentIp
    {
        get => _currentIp;
        private set => this.RaiseAndSetIfChanged(ref _currentIp, value);
    }

    public Organization? SelectedOrganization
    {
        get => _selectedOrganization;
        set => this.RaiseAndSetIfChanged(ref _selectedOrganization, value);
    }

    public ObservableCollection<Organization> Organizations { get; }
    public ObservableCollection<ItemPosition<IpAddress>> FavoriteIps { get; }

    public ReactiveCommand<Unit, Unit> ShowHomePageCommand { get; }
    public ReactiveCommand<string?, Task> AddIpToFavoritesCommand { get; }
    public ReactiveCommand<string?, Task> FindEntityCommand { get; }

    private async Task AddIpToFavoritesAsync(string? ip)
    {
        if (string.IsNullOrEmpty(ip) || FavoriteIps.Any(t => t.Item.Ip == ip))
            return;

        var ipAddress = new IpAddress(ip, IpInfo);

        int position = FavoriteIps.Count + 1;
        var itemPosition = new ItemPosition<IpAddress>(ipAddress, position);

        try
        {
            await FavoriteIpService.AddFavoriteIpAddressAsync(ipAddress);
            FavoriteIps.Add(itemPosition);
        }
        catch (Exception ex)
        {
            string message = $"Failed to add IP to favorites. Reason: {ex.Message}";
            PopupService.ShowErrorInUIThread(message);
        }
    }

    private async Task FindEntityAsync(string? pattern)
    {
        if (string.IsNullOrEmpty(pattern))
            return;

        bool isIpAddress = IPAddress.TryParse(pattern, out _);

        if (isIpAddress)
            await FindIpAsync(pattern);
        else
            await FindOrganizationsAsync(pattern);
    }

    private async Task FindOrganizationsAsync(string? pattern)
    {
        if (string.IsNullOrEmpty(pattern))
            return;

        Organization[] foundOrganizations = [];

        try
        {
            foundOrganizations = await OrganizationService
                .GetOrganizationsAsync(pattern, _max_organizations_count)
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            string message = $"Failed to find organizations. Reason: {ex.Message}";
            PopupService.ShowErrorInUIThread(message);
        }

        if (foundOrganizations.Length == 0)
        {
            string message = $"Organizations not found.";
            PopupService.ShowInfoInUIThread(message);
            return;
        }

        Organizations.Clear();

        foreach (Organization organization in foundOrganizations)
            Organizations.Add(organization);
    }

    private async Task FindIpAsync(string? ip)
    {
        if (string.IsNullOrEmpty(ip))
            return;

        try
        {
            IpAddress ipDetails = await IpDetailsService.GetIpDetailsAsync(ip);
            CurrentIp = ipDetails.Ip;
            IpInfo = ipDetails.Details;
        }
        catch (Exception ex)
        {
            string message = $"IP not found. Reason: {ex.Message}";
            PopupService.ShowErrorInUIThread(message);
        }
    }
}
