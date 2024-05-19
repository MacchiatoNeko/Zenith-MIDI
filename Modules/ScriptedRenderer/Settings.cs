namespace ScriptedRender
{
    public class Settings
    {
        public int firstNote = 0;
        public int lastNote = 128;
        public double deltaTimeOnScreen = 300;

        public string palette = "Random";

        public Script currScript = null;
        public long lastScriptChangeTime = 0;
    }
}
