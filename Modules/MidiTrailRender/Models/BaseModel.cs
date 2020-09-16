﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDITrailRender.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public CameraModel Camera { get; set; } = new CameraModel();
        public GeneralModel General { get; set; } = new GeneralModel();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
