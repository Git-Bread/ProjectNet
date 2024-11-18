using Newtonsoft.Json;
namespace ProjectNet
{
    public class Saver
    {
        //saves the character to character file, has all user and floor data
        public static void SaveGame()
        {
            ExampleCharacterSheet character = new();
            File.WriteAllText("config/character.json", JsonConvert.SerializeObject(character));
        }

        //loads the game and setts the content to the "global" class that is used around the game
         public static void LoadGame()
        {
            ExampleCharacterSheet character = JsonConvert.DeserializeObject<ExampleCharacterSheet>(File.ReadAllText("config/character.json"));
            if (character == null)
            {
                return;
            }
            if(character.floor.level != -1)
            {
                CharacterSheet.agility = character.agility;
                CharacterSheet.life = character.life;
                CharacterSheet.strength = character.strength;
                CharacterSheet.alignment = character.alignment;
                CharacterSheet.weapon = character.weapon;
                CharacterSheet.floor = character.floor;
                CharacterSheet.pain = character.pain;
                CharacterSheet.key = character.key;
            }
        }

        //wipes the save by overwriting with empty string, and then loads that to overwrite the values
        public static void RemoveSave()
        {
            File.WriteAllText("config/character.json", String.Empty);
            LoadGame();
        }

        //character sheet copy used for object handling, since static variables cannot be copied and used as refrences.
        public class ExampleCharacterSheet
        {
            public int agility;
            public int life;
            public int strength;
            public string alignment;
            public Weapon weapon;
            public Floor floor;
            public int pain;
            public bool key;
            public ExampleCharacterSheet()
            {
                agility = CharacterSheet.agility;
                life = CharacterSheet.life;
                strength = CharacterSheet.strength;
                alignment = CharacterSheet.alignment;
                weapon = CharacterSheet.weapon;
                floor = CharacterSheet.floor;
                pain = CharacterSheet.pain;
                key = CharacterSheet.key;
        }
        }

        //saves the settings
        public static void SaveSettings()
        {
            ExampleSaveSettings settings = new();
            File.WriteAllText("config/settings.json", JsonConvert.SerializeObject(settings));
        }
        //loads the settings
        public static void LoadSettings()
        {
            ExampleSaveSettings settings = JsonConvert.DeserializeObject<ExampleSaveSettings>(File.ReadAllText("config/settings.json")) ?? new();
            if (settings == null)
            {
                return;
            }
            Settings.speed = settings.speed;
            Settings.lineSize = settings.lineSize;
            Settings.rank = settings.rank;
            Settings.skipIntro = settings.skipIntro;
        }
        //same logic as charactersheet save handling
        public class ExampleSaveSettings
        {
            public int speed;
            public int lineSize;
            public string rank;
            public bool skipIntro;
            public ExampleSaveSettings()
            {
                speed = Settings.speed;
                lineSize = Settings.lineSize;
                rank = Settings.rank;
                skipIntro = Settings.skipIntro;
            }
        }
    }
}