using System;
using System.IO;
using System.Threading.Tasks;
using ZenithEngine;
using ZenithEngine.Modules;
using ZenithEngine.MIDI;
using ZenithEngine.MIDI.Disk;

namespace Zenith
{
    public class Handler
    {
#pragma warning disable CA1859
        private MidiFile midifile;
        public bool MidiLoaded;
        public string OutputPath;
        ModuleManager ModuleRunner { get; } = new ModuleManager();
        public RenderPipeline ActivePipeline;
        RenderStatus CurrentRenderStatus { get; set; } = null;

        public void LoadMidi(string path)
        {
            if (midifile != null) UnloadMidi(); // Make sure that previous MIDI File is not loaded, unload if it is loaded.
            if (!File.Exists(path)) // Check if MIDI file exists.
            {
                Console.WriteLine("Midi file doesn't exist");
                return;
            }
            try
            {
                midifile?.Dispose(); 
                midifile = null;
                // ^^^ Dispose MidiFile class and set it as null.
                GC.Collect();
                GC.WaitForFullGCComplete();
                // ^^^ Forces an immediate garbage collection of all generations, reducing memory usage, waits for it to be completed.
                midifile = new DiskMidiFile(path); // Load here, without checks.
                MidiLoaded = true; // Set loaded status.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void UnloadMidi()
        {
            midifile.Dispose();
            midifile = null;
            // ^^^ Dispose MidiFile class and set it as null.
            GC.Collect();
            GC.WaitForFullGCComplete();
            // ^^^ Forces an immediate garbage collection of all generations, reducing memory usage, waits for it to be completed.
            MidiLoaded = false; // Set loaded status.
        }

        public void SetPipelineValues()
        {
            if (ActivePipeline == null) return;
            if (!ActivePipeline.Rendering)
            {
                ActivePipeline.Paused = false;
                ActivePipeline.PreviewSpeed = 1;
                ActivePipeline.VSync = true;
                ActivePipeline.Status.RealtimePlayback = true;
            }
            else
            {
                ActivePipeline.Paused = false;
                ActivePipeline.PreviewSpeed = 1;
                ActivePipeline.VSync = false;
                ActivePipeline.Status.RealtimePlayback = false;
            }
            ActivePipeline.Status.FPS = DefaultValues.FPS;
        }

        public void StartPipeline(bool render)
        {
            var timeBased = DefaultValues.TimeBased; // TODO: Get value from user
            // var startOffset = midifile.StartTicksToSeconds(ModuleRunner.CurrentModule.StartOffset, timeBased) +
            //                (render ? (double)secondsDelay.Value : 0);
            var startOffset = 0;

            var playback = midifile.GetMidiPlayback(
                startOffset,
                timeBased
            );

            CurrentRenderStatus = new RenderStatus(DefaultValues.Width, DefaultValues.Width, 1); // TODO: Make SSAA settable

            if (render)
            {
                var ffmpegArgs = string.Empty; // TODO: Set arg prefix
                // ffmpegArgs = $"-itsoffset {startOffset.ToString().Replace(",", ".")} -i \"{audioPath.Text}\" -acodec aac {ffmpegArgs}"; <-- add this on the end if audio included
                string SetOutputPath = null; // TODO: Get path from user
                if (SetOutputPath == null)
                {
                    OutputPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $"/Zenith_{DateTime.Now:yyyyMMdd_HHmmss}.mp4";
                } 
                else
                {
                    OutputPath = SetOutputPath;
                }
                var args = new OutputSettings(ffmpegArgs, OutputPath, false ? "" : null);
                ActivePipeline = new RenderPipeline(CurrentRenderStatus, playback, ModuleRunner, args);
            }
            else
            {
                ActivePipeline = new RenderPipeline(CurrentRenderStatus, playback, ModuleRunner);
            }

            SetPipelineValues();
            var thread = ActivePipeline.Start(null);
            Task.Run(thread.Wait);
        }
        
        public void Stop()
        {
            if (ActivePipeline != null)
            {
                ActivePipeline.Dispose();
                ActivePipeline = null;
            }
        }
    }
}
