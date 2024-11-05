namespace ProjectNet
{
    public class TextGame
    {
        public static void Main(string[] args)
        {
            //console windows only for resize, linux be damned
            Console.Title = "The Dark Tower";

            intro.playIntro();

            menu();
        }

        public void menu()
        {
            Console.Clear();
            TextFunctions.slowPrint("You stand before");
            TextFunctions.slowPrint("You stand before");
            TextFunctions.slowPrint("You stand before");
            TextFunctions.slowPrint("You stand before");
            TextFunctions.slowPrint("You stand before");
        }
    }
}