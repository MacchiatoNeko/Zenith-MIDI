using OpenTK.Graphics;
using System;
using ZenithEngine.GLEngine;
using ZenithEngine.MIDI;

namespace ZenithEngine.Modules
{
    public interface IModuleRender : IDisposable
    {
        string Name { get; }
        string Description { get; }
        bool Initialized { get; }
        string LanguageDictName { get; }
        public double StartOffset { get; }

        void Init(MidiPlayback midi, RenderStatus status);
        void RenderFrame(RenderSurface renderSurface);
        void ReloadTrackColors();
    }
}