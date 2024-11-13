namespace ProjectNet
{
    public class GameStart
    {
        public static void Introduction()
        {
            #region First Paragraph
            Console.Clear();
            Settings.wordCounter = 0;
            Console.WriteLine();
            TextFunctions.SlowPrint(" The rolling of ");
            TextFunctions.SlowPrint("Thunder ", "yellow");
            TextFunctions.SlowPrint("stirs you from your troubled sleep. ");
            TextFunctions.SlowPrint("You hear the pitter-patter of the rain against the wooden roof of the ");
            TextFunctions.SlowPrint("Carriage, ", "brown");
            TextFunctions.SlowPrint("and the soft smell of petrichor seems to have all but overtaken the tiny room. ");
            #endregion
            #region Second Paragraph
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" Still feeling drowsy from the days of non-stop travel within this confined space, you take respite in the only ");
            TextFunctions.SlowPrint("Luxury ", "green");
            TextFunctions.SlowPrint("you are afforded. A small window opposite of you through which the dim ");
            TextFunctions.SlowPrint("Moonlight ", "white");
            TextFunctions.SlowPrint("tricles through, ");
            TextFunctions.SlowPrint("Illuminating ", "yellow");
            TextFunctions.SlowPrint("the interior of the ");
            TextFunctions.SlowPrint("Carriage.", "brown");
            #endregion
            #region Third Paragraph
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" The ");
            TextFunctions.SlowPrint("Tower ", "purple");
            TextFunctions.SlowPrint("draws closer with every day, soon you will face its trials. ");
            TextFunctions.SlowPrint("You ponder on this crossroad of faith, what do you fight for? And upon finding the much needed ");
            TextFunctions.SlowPrint("Answer, ", "yellow");
            TextFunctions.SlowPrint("you vow to return ");
            TextFunctions.SlowPrint("Alive ", "green");
            TextFunctions.SlowPrint("to...");
            #endregion
            #region Options
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" 1. Reunite with your beloved ");
            TextFunctions.SlowPrint("Family.", "green");
            Console.WriteLine("");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" 2. Restore your shattered ");
            TextFunctions.SlowPrint("Reputation.", "yellow");
            Console.WriteLine("");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" 3. ");
            TextFunctions.SlowPrint("Vanquish ", "red");
            TextFunctions.SlowPrint("those who condemned you.");
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" What weights heavy on your heart?");
            Console.WriteLine("\n");
            #endregion

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1)
                {
                    Settings.alignment = "good";
                    break;
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    Settings.alignment = "neutral";
                    break;
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    Settings.alignment = "evil";
                    break;
                }
            }
            WeaponPick();
        }
        public static void WeaponPick()
        {
            #region First Paragraph
            Console.Clear();
            Settings.wordCounter = 0;
            Console.WriteLine();
            TextFunctions.SlowPrint(" The ");
            TextFunctions.SlowPrint("Carriage ", "brown");
            TextFunctions.SlowPrint("grinds to a halt and the two ");
            TextFunctions.SlowPrint("Armed Guards ", "white");
            TextFunctions.SlowPrint("escort you out of your confine. ");
            TextFunctions.SlowPrint("The ");
            TextFunctions.SlowPrint("Tower ", "purple");
            TextFunctions.SlowPrint("stands as an ");
            TextFunctions.SlowPrint("Obsidian ", "purple");
            TextFunctions.SlowPrint("monolith piercing the very heavens in ");
            TextFunctions.SlowPrint("Defiance. ");
            #endregion
            #region Second Paragraph
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" The ");
            TextFunctions.SlowPrint("Tower ", "purple");
            TextFunctions.SlowPrint("is featureless, all its six sides are impossibly smooth and no windows or even holes can be seen across its surface. ");
            TextFunctions.SlowPrint("The only detail of note is a pair of double doors made out of wood ");
            TextFunctions.SlowPrint("Wood ", "brown");
            TextFunctions.SlowPrint("that seems hopelessly out of place on the  ");
            TextFunctions.SlowPrint("Obsidian ", "purple");
            TextFunctions.SlowPrint("Behemoth.");
            #endregion
            #region Third Paragraph
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" You are corraled to the front of the ");
            TextFunctions.SlowPrint("Tower, ", "purple");
            TextFunctions.SlowPrint("where a clerk absent mindely looks through ledgers. ");
            TextFunctions.SlowPrint("Upon noticing your presence he uncaringly points towards a dillapidated ");
            TextFunctions.SlowPrint("weapons rack ", "white");
            TextFunctions.SlowPrint("to the right of the doors.");
            #endregion
            #region Fourth Paragraph
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" The ");
            TextFunctions.SlowPrint("Arnaments ", "white");
            TextFunctions.SlowPrint("show large signs of neglect and exposure to the elements, but none the less, you gotta work with what you got.");
            TextFunctions.SlowPrint("Two ");
            TextFunctions.SlowPrint("Weapons ", "white");
            TextFunctions.SlowPrint("are in suprisingly good condition, you may pick one.");
            #endregion
            #region Options
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" 1. The Spear ");
            TextFunctions.SlowPrint("Sharp and Defensive. ", "white");
            TextFunctions.SlowPrint("Its length gives many advantages and it excels in ");
            TextFunctions.SlowPrint("Thrusting Attacks.", "green");
            Console.WriteLine("");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" 2. The Sword ");
            TextFunctions.SlowPrint("Offensive and Light ", "white");
            TextFunctions.SlowPrint("its versatility and shape make it perfect for ");
            TextFunctions.SlowPrint("Slashing Attacks", "green");
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" Which one shall be your companion?");
            Console.WriteLine("\n");
            #endregion

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1)
                {
                    Weapon spear = new("Trusty Spear", 3, false);
                    Settings.weapon = spear;
                    break;
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    Weapon sword = new("Light Blade", 4, true);
                    Settings.weapon = sword;
                    break;
                }
            }
        }
    }
}