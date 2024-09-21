namespace StarWars.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        FavoriteEntitiesViewModel = new FavoriteEntitiesViewModel();

        FilmsViewModel = new FilmsViewModel(FavoriteEntitiesViewModel);
        CharactersViewModel = new CharactersViewModel(FavoriteEntitiesViewModel);
        PlanetsViewModel = new PlanetsViewModel(FavoriteEntitiesViewModel);
        SpeciesViewModel = new SpeciesViewModel(FavoriteEntitiesViewModel);
        StarshipsViewModel = new StarshipsViewModel(FavoriteEntitiesViewModel);
        VehiclesViewModel = new VehiclesViewModel(FavoriteEntitiesViewModel);
    }

    public FilmsViewModel FilmsViewModel { get; }
    public CharactersViewModel CharactersViewModel { get; }
    public PlanetsViewModel PlanetsViewModel { get; }
    public SpeciesViewModel SpeciesViewModel { get; }
    public StarshipsViewModel StarshipsViewModel { get; }
    public VehiclesViewModel VehiclesViewModel { get; }
    public FavoriteEntitiesViewModel FavoriteEntitiesViewModel { get; }
}
