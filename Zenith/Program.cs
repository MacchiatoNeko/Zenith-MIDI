using System;

namespace Zenith
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Zenith Console";
            Handler instance = new();
            instance.LoadMidi("~/home/macchiatoneko/Downloads/feathers_flying_off_the_beaten_path.mid");
            instance.SetPipelineValues();
            instance.StartPipeline(true);
        }
    }
}
