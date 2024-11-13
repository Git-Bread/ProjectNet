
using System.Text.Json;

namespace ProjectNet
{
    public class Floor
    {
        public int level = -1;
        public List<string> objects = new();
        public Monster opponent = new("0", "0", 0, 0, 0, 0);
    }
    public class Monster
    {
        public string Name;
        public string Description;
        public int HP;
        public int Damage;
        public int DefenseType;
        public int DifficultyTier;

        public Monster(string name, string description, int hp, int damage, int defenseType, int difficultyTier)
        {
            Name = name;
            Description = description;
            HP = hp;
            Damage = damage;
            DefenseType = defenseType;
            DifficultyTier = difficultyTier;
        }
    }


    public class GameGen
    {
        public static Floor GenerateFloor()
        {
            Random random = new();
            Floor floor = new();
            int itemCount = random.Next(3);
            List<int> objectNumbers = new();
            int level = CharacterSheet.floor.level + 1;
            floor.level = level;
            for (int i = 0; i < itemCount; i++)
            {
                objectNumbers.Add(random.Next(6));
            }
            string[] objects = { "Table", "Chair", "Bed", "Fireplace", "Chest" };
            for (int i = 0; i < objectNumbers.Count; i++)
            {
                floor.objects.Add(objects[objectNumbers[i]]);
            }

            string json = File.ReadAllText("config/monsters.json");
            List<Monster> monsterList = JsonSerializer.Deserialize<List<Monster>>(json) ?? new();
            List<Monster> sortedMonsterList = monsterList.FindAll(monster => monster.DifficultyTier == level);
            floor.opponent = sortedMonsterList[random.Next(sortedMonsterList.Count)];

            return new Floor();
        }

    }
}