using ZenithEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using ZenithEngine.Modules;
using ZenithEngine.MIDI;
using ZenithEngine.MIDI.Disk;
using System.Collections.ObjectModel;

namespace Zenith
{
    public class Handler
    {
        private MidiFile midifile;
        private string midipath = null;
        private bool MidiLoaded = false;

        ObservableCollection<IModuleRender> RenderModules { get; } = new ObservableCollection<IModuleRender>();

        long lastBackgroundChangeTime = 0;

        private readonly string defaultPlugin = "Classic";

        ModuleManager ModuleRunner { get; } = new ModuleManager();

        public RenderPipeline ActivePipeline;

        RenderStatus CurrentRenderStatus { get; set; } = null;

        public void LoadMidi(string path)
        {
            if (midifile != null) UnloadMidi();

            if (!File.Exists(path))
            {
                Console.WriteLine("Midi file doesn't exist");
                return;
            }
            try
            {
                midifile?.Dispose(); // Unload MIDI file if it's already loaded.
                midifile = null;
                GC.Collect();
                GC.WaitForFullGCComplete();
                midifile = new DiskMidiFile(midipath); // Load here, without checks NOTE -> Change to path variable after portage.
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
            GC.Collect();
            GC.WaitForFullGCComplete();
            MidiLoaded = false;
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
            var timeBased = false;
            // var startOffset = midifile.StartTicksToSeconds(ModuleRunner.CurrentModule.StartOffset, timeBased) +
            //                (render ? (double)secondsDelay.Value : 0);
            var startOffset = 0;

            var playback = midifile.GetMidiPlayback(
                startOffset,
                timeBased
            );

            CurrentRenderStatus = new RenderStatus(DefaultValues.Width, DefaultValues.Width, 1);

            if (render)
            {
                var ffmpegArgs = string.Empty; // TODO: Additional ffmpeg args, pass them later
                // ffmpegArgs = $"-itsoffset {startOffset.ToString().Replace(",", ".")} -i \"{audioPath.Text}\" -acodec aac {ffmpegArgs}";
                var args = new OutputSettings(ffmpegArgs, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $"/Video_{DateTime.Now:yyyyMMdd_HHmmss}.mp4", false ? "" : null);
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
