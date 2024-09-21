using HarryPotter.Models;
using HarryPotter.Services;
using HarryPotter.ViewModels.Base;

namespace HarryPotter.ViewModels;

public class SpellsViewModel()
    : EntitiesViewModel<Spell>(HogwartsEntitiesService.Spells)
{
}
