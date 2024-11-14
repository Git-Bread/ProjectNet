
namespace ProjectNet
{
    public class CharacterSheet
    {
        internal static int agility = 1;
        internal static int life = 5;
        internal static int strength = 1;
        internal static string alignment = "neutral";
        internal static Weapon weapon = new("null", 0, 0);
        internal static Floor floor = new();
        internal static int pain = 0;
        internal static bool key = false;
    }
}