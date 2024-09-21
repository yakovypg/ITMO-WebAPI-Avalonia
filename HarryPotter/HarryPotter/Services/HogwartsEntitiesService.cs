using HarryPotter.DataAccess;
using HarryPotter.Models;

namespace HarryPotter.Services
{
    public static class HogwartsEntitiesService
    {
        static HogwartsEntitiesService()
        {
            LikedCharactersHandler = new LikedCharactersHandler();
            LikedCharacters = new LikedCharactersRepository();
            Characters = new CharactersRepository();
            HogwartsStudents = new HogwartsStudentsRepository();
            HogwartsStaff = new HogwartsStaffRepository();
            CharactersInHouse = new CharactersInHouseRepository();
            Spells = new SpellsRepository();
        }

        public static IHogwartsEntityHandler<Character> LikedCharactersHandler { get; }
        public static IHogwartsEntityRepository<Character> LikedCharacters { get; }
        public static IHogwartsEntityRepository<Character> Characters { get; }
        public static IHogwartsEntityRepository<Character> HogwartsStudents { get; }
        public static IHogwartsEntityRepository<Character> HogwartsStaff { get; }
        public static IHogwartsEntityRepository<Character> CharactersInHouse { get; }
        public static IHogwartsEntityRepository<Spell> Spells { get; }
    }
}
