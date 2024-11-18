namespace ProjectNet
{
    //settings class that is referenced around the project
    class Settings
    {
        //word printing speed, base is 3
        internal static int speed = 3;
        //word counter, used to controll line length, not very smart solution
        internal static int wordCounter = 0;
        //maximum linesize of sentence
        internal static int lineSize = 50;
        //funny rank as a cosmetic
        internal static string rank = "Baron";
        //to skip, or not to skip the intro
        internal static bool skipIntro = false;
        //used for menu loading logic, to speed upp printing of duplicates
        internal static bool[] wasInSettings = { false, false };
    }

    //weapon object, self explanatory
    public class Weapon
    {
        public string name = "";
        public int damage = 0;
        public int type = 0; //1 for sword 0 for spear

        public Weapon(string v1, int v2, int v3)
        {
            this.name = v1;
            this.damage = v2;
            this.type = v3;
        }
    }
}