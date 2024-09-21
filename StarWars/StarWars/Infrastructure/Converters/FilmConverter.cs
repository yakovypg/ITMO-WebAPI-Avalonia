using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace StarWars.Infrastructure.Converters
{
    public class FilmConverter : IEntityDataConverter<FilmData, Film>
    {
        public Film Convert(FilmData entityData)
        {
            ArgumentNullException.ThrowIfNull(entityData, nameof(entityData));

            DateTime? createdDate = null;
            DateTime? editedDate = null;
            DateTime? releaseDate = null;

            if (DateTime.TryParse(entityData.Created, out DateTime parsedCreatedDate))
                createdDate = parsedCreatedDate;

            if (DateTime.TryParse(entityData.Edited, out DateTime parsedEditedDate))
                editedDate = parsedEditedDate;

            if (DateTime.TryParse(entityData.ReleaseDate, out DateTime parsedReleaseDate))
                releaseDate = parsedReleaseDate;

            string openingCrawl = NameService.RemoveDoubleSpaces(
                entityData.OpeningCrawl?.ReplaceLineEndings(" ").Trim());

            return new Film(
                createdDate,
                editedDate,
                entityData.Url ?? string.Empty,
                new(entityData.Characters),
                entityData.Director ?? string.Empty,
                entityData.EpisodeId,
                openingCrawl,
                new(entityData.Planets),
                entityData.Producer?.Split(", ").ToList() ?? [],
                releaseDate,
                new(entityData.Species),
                new(entityData.Starships),
                entityData.Title ?? string.Empty,
                new(entityData.Vehicles));
        }

        public bool TryConvert(FilmData entityData, [NotNullWhen(true)] out Film? entity)
        {
            try
            {
                entity = Convert(entityData);
                return true;
            }
            catch
            {
                entity = null;
                return false;
            }
        }
    }
}
