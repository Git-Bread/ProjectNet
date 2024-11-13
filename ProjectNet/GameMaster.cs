namespace ProjectNet
{
    public class GameMaster
    {
        public static void RunGame()
        {
            Console.Clear();
            while(true)
            {
                CharacterSheet.floor = GameGen.GenerateFloor();
                Floor floor= CharacterSheet.floor;
                #region Description
                Settings.wordCounter = 0;
                TextFunctions.SlowPrint(" You are in an square room with a narrow hallway leading forward... ");
                TextFunctions.SlowPrint("At the end of the passage you see another room and a ");
                TextFunctions.SlowPrint($"{floor.opponent.Name}, ", "red");
                TextFunctions.SlowPrint($"looking menacingly at you. ");
                Settings.wordCounter = 0;
                Console.WriteLine("\n");
                if (floor.level == 0)
                {
                    TextFunctions.RowPrint(" you may take any of these actions by inputing similar words or sentences: ");
                    Console.WriteLine();
                    TextFunctions.RowPrint(" When the opponent is vanquished, you may ascend the stairs.");
                    TextFunctions.RowPrint(" Run away from the fight, sometimes its better to try again.");
                    TextFunctions.RowPrint(" Advance down the hallway, its do or die.");
                    TextFunctions.RowPrint(" Rest, helps if theres something to rest on.");
                    TextFunctions.RowPrint(" Look around, might be something of value.");
                    TextFunctions.RowPrint(" If you find a chest you can try opening it.");
                    Console.WriteLine("\n");
                }
                #endregion
                Console.WriteLine("");
                Predicter.Determine(Console.ReadLine() ?? "null");
                Console.WriteLine("\n");
            }
        }
    }
}