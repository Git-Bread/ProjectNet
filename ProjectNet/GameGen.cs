namespace ProjectNet
{
    public class Floor
    {
        public int level = 0;
        public List<string> objects = new();
        public int opponent = 0;
    }
    public class GameGen
    {
        public static Floor GenerateFloor()
        {
            Random random = new();
            Floor floor = new();
            int itemCount = random.Next(3);
            List<int> objectNumbers = new();
            for (int i = 0; i < itemCount; i++)
            {
                objectNumbers.Add(random.Next(6));
            }
            string[] objects = {"Furniture1", "Furniture2", "Furniture3", "Furniture4", "Chest"};
            for (int i = 0;i < objectNumbers.Count; i++)
            {
                floor.objects.Add(objects[objectNumbers[i]]);
            }
            floor.opponent = random.Next(10);

            return new Floor();
        }
    }
}