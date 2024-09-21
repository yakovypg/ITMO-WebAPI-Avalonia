using Avalonia.Media.Imaging;
using HarryPotter.Infrastructure.Converters;
using HarryPotter.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarryPotter.Models;

public class Character : HogwartsEntity
{
    private Bitmap? _bitmapImage;
    
    public string? Name { get; set; }
    public string? Species { get; set; }
    public string? Gender { get; set; }
    public string? House { get; set; }
    public string? Ancestry { get; set; }
    public string? EyeColour { get; set; }
    public string? Patronus { get; set; }
    public string? Actor { get; set; }
    public string? Image { get; set; }

    public Wand? Wand { get; set; }

    public bool? Wizard { get; set; }
    public bool? HogwartsStudent { get; set; }
    public bool? HogwartsStaff { get; set; }
    public bool? Alive { get; set; }
    public int? YearOfBirth { get; set; }

    [JsonConverter(typeof(HarryPotterDateTimeConverter))]
    public DateTime? DateOfBirth { get; set; }

    [JsonProperty("alternate_names")]
    public List<string> AlternateNames { get; set; } = [];

    [JsonProperty("alternate_actors")]
    public List<string> AlternateActors { get; set; } = [];

    [JsonIgnore]
    public Bitmap? BitmapImage
    {
        get => _bitmapImage;
        private set => RaiseAndSetIfChanged(ref _bitmapImage, value);
    }

    [JsonIgnore]
    public List<Tuple<string, object?>> Properties => [
        new(nameof(Name), Name),
        new(nameof(Species), Species),
        new(nameof(Gender), Gender),
        new(nameof(House), House),
        new(nameof(Ancestry), Ancestry),
        new(nameof(EyeColour), EyeColour),
        new(nameof(Patronus), Patronus),
        new(nameof(Actor), Actor),
        new(nameof(Wand), Wand),
        new(nameof(Wizard), Wizard),
        new(nameof(HogwartsStudent), HogwartsStudent),
        new(nameof(HogwartsStaff), HogwartsStaff),
        new(nameof(Alive), Alive),
        new(nameof(YearOfBirth), YearOfBirth),
        new(nameof(DateOfBirth), DateOfBirth?.ToShortDateString()),
        new(nameof(AlternateNames), string.Join(", ", AlternateNames)),
        new(nameof(AlternateActors), string.Join(", ", AlternateActors)),
    ];

    public async Task LoadBitmapImageAsync()
    {
        BitmapImage = await BitmapService.LoadFromWebAsync(Image);
    }
}
