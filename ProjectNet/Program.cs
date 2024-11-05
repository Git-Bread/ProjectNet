namespace ProjectNet
{
    public class TextGame
    {
        public static void Main(string[] args)
        {
            //console windows only for resize, linux be damned
            Console.Title = "The Dark Tower";
            if (!settings.SkipIntro)
            {
                Intro.PlayIntro();
            }
            Menu.MenuDisplay();
        }
    }
}