
namespace ProjectNet
{
    //global character "data" that can be accessed and overwritten from anywhere, might be bad praxis but works well.
    public class CharacterSheet
    {
        //internal might be better than public in closed assemblys, but i might be wrong so gonna use both :)

        //agility is a damage reduction stat
        internal static int agility = 1;
        //base maximum life
        internal static int life = 5;
        //life modifier
        internal static int strength = 1;
        //alignement was gonna be used for unqiue end text, but i dident bother
        internal static string alignment = "neutral";
        //characters weapon { name, damage, type }
        internal static Weapon weapon = new("null", 0, 0);
        //the current floor
        internal static Floor floor = new();
        //if character has stuff like backpain
        internal static int pain = 0;
        //key is a bool simply because the odds of running into several locked chests is practically non-existant
        internal static bool key = false;
    }
}