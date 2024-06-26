﻿using SharpDX;
using System.ComponentModel;

namespace MIDITrailRender.Models
{
    public class LightModel : INotifyPropertyChanged
    {
        public double LightX { get; set; } = 0;
        public double LightZ { get; set; } = 0;
        public double Strength { get; set; } = 1;

        public Vector3 ToVector()
        {
            return new Vector3((float)LightX, 1, (float)LightZ);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
