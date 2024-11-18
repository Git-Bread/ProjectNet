using Newtonsoft.Json;
namespace ProjectNet
{
    public class Saver
    {
        public static void SaveGame()
        {

        }
         public static void LoadGame()
        {

        }
        public static void RemoveSave()
        {
            File.WriteAllText("config/character.json", String.Empty);
        }
        public static void SaveSettings()
        {
            ExampleSaveSettings settings = new();
            File.WriteAllText("config/settings.json", JsonConvert.SerializeObject(settings));
        }
        public static void LoadSettings()
        {
            ExampleSaveSettings settings = JsonConvert.DeserializeObject<ExampleSaveSettings>(File.ReadAllText("config/settings.json")) ?? new();
            Settings.speed = settings.speed;
            Settings.lineSize = settings.lineSize;
            Settings.rank = settings.rank;
            Settings.skipIntro = settings.skipIntro;
        }
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