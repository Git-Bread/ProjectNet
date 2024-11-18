using Newtonsoft.Json;

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
        public string Name { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }
        public int DefenseType { get; set; }
        public int DifficultyTier { get; set; }

        public Monster(string name, string description, int hp, int damage, int defense_type, int difficulty_tier)
        {
            this.Name = name;
            this.Description = description;
            this.HP = hp;
            this.Damage = damage;
            this.DefenseType = defense_type;
            this.DifficultyTier = difficulty_tier;
        }
    }


    public class GameGen
    {
        public static Floor GenerateFloor()
        {
            Random random = new();
            Floor floor = new();
            int itemCount = random.Next(3);
            if(itemCount == 0)
            {
                itemCount = random.Next(2);
            }
            List<int> objectNumbers = new();
            int level = CharacterSheet.floor.level + 1;
            floor.level = level;
            for (int i = 0; i < itemCount; i++)
            {
                int next = random.Next(6);
                if (!objectNumbers.Contains(next))
                {
                    objectNumbers.Add(random.Next(5));
                }
            }
            string[] objects = { "Table", "Chair", "Bed", "Fireplace", "Chest" };
            for (int i = 0; i < objectNumbers.Count; i++)
            {
                floor.objects.Add(objects[objectNumbers[i]]);
            }

            List<Monster> monsterList = JsonConvert.DeserializeObject<List<Monster>>(File.ReadAllText("config/monsters.json")) ?? new();
            List<Monster> sortedMonsterList = monsterList.FindAll(monster => monster.DifficultyTier == level);
            floor.opponent = sortedMonsterList[random.Next(sortedMonsterList.Count)];
            
            return floor;
        }

    }
}