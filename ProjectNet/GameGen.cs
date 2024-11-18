using Newtonsoft.Json;

namespace ProjectNet
{
    //game "floor" object
    public class Floor
    {
        //level -1 is default non-floor
        public int level = -1;
        //floor objects
        public List<string> objects = new();
        //floor monster
        public Monster opponent = new("0", "0", 0, 0, 0, 0);
    }
    //monster class
    public class Monster
    {
        //monsters stats and description, i have get; set; because i read somewhere it was good praxis, but im not sure why, since its not private.
        public string Name { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }
        //defense type is for weakness to thrust/slash
        public int DefenseType { get; set; }
        //difficulty tier determines the "floor" the enemy can occur at
        public int DifficultyTier { get; set; }

        //constructor for stats
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

    //Floor Generator
    public class GameGen
    {
        //one of the few functions with an actual return, not the greatest structure :)
        public static Floor GenerateFloor()
        {
            //variables setup
            Random random = new();
            Floor floor = new();
            int itemCount = random.Next(3);

            //empty floor should exist, but not be a 33% so a cointoss to add one item if its empty
            if(itemCount == 0)
            {
                itemCount = random.Next(2);
            }
            //list of numbers to determine objects
            List<int> objectNumbers = new();

            //base floor is -1 so as-soon as this is run it becomes atleast 0 with incrementing for every time its called again
            int level = CharacterSheet.floor.level + 1;
            floor.level = level;

            //runs through list of numbers and adds to list, only if its not already occuring
            for (int i = 0; i < itemCount; i++)
            {
                int next = random.Next(6);
                if (!objectNumbers.Contains(next))
                {
                    objectNumbers.Add(random.Next(5));
                }
            }

            //adds objects depending on earlier numbers
            string[] objects = { "Table", "Chair", "Bed", "Fireplace", "Chest" };
            for (int i = 0; i < objectNumbers.Count; i++)
            {
                floor.objects.Add(objects[objectNumbers[i]]);
            }

            //read monster json file and filter through it for all monsters of appropriate level, whereafter it randomly gets one
            List<Monster> monsterList = JsonConvert.DeserializeObject<List<Monster>>(File.ReadAllText("config/monsters.json")) ?? new();
            List<Monster> sortedMonsterList = monsterList.FindAll(monster => monster.DifficultyTier == level);
            floor.opponent = sortedMonsterList[random.Next(sortedMonsterList.Count)];
            
            return floor;
        }

    }
}