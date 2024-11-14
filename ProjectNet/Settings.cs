namespace ProjectNet
{
    public class Settings
    {
        internal static int speed = 100;
        internal static int wordCounter = 0;
        internal static int lineSize = 50;
        internal static string rank = "Baron";
        internal static bool skipIntro = true;
        internal static bool[] wasInSettings = { false, false };
        internal static int oldSpeed = 0;
        internal static bool firstTime = true;
    }

    class Weapon
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