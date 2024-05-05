using System;

namespace Zenith
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Zenith Console";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"  ______          _ _   _     
 |___  /         (_) | | |    
    / / ___ _ __  _| |_| |__  
   / / / _ \ '_ \| | __| '_ \ 
  / /_|  __/ | | | | |_| | | |
 /_____\___|_| |_|_|\__|_| |_|");
            Console.ForegroundColor = ConsoleColor.White;
            Handler instance = new();
            instance.LoadMidi("~/home/macchiatoneko/Downloads/feathers_flying_off_the_beaten_path.mid");
            instance.SetPipelineValues();
            instance.StartPipeline(true);
        }
    }
}
