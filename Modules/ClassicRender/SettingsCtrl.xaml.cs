﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace ClassicRender
{
    /// <summary>
    /// Interaction logic for SettingsCtrl.xaml
    /// </summary>
    public partial class SettingsCtrl : UserControl
    {
        Settings settings;

        public event Action PaletteChanged
        {
            add { paletteList.PaletteChanged += value; }
            remove { paletteList.PaletteChanged -= value; }
        }

        public void SetValues()
        {
            firstNote.Value = settings.firstNote;
            lastNote.Value = settings.lastNote - 1;
            pianoHeight.Value = (int)(settings.pianoHeight * 100);
            noteDeltaScreenTime.Value = settings.deltaTimeOnScreen;
            sameWidth.IsChecked = settings.sameWidthNotes;
            blackNotesAbove.IsChecked = settings.blackNotesAbove;
            paletteList.SelectImage(settings.palette);
        }

        public SettingsCtrl(Settings settings) : base()
        {
            InitializeComponent();
            noteDeltaScreenTime.NudToSlider = v => Math.Log(v, 2);
            noteDeltaScreenTime.SliderToNud = v => Math.Pow(2, v);
            this.settings = settings;
            paletteList.SetPath("Plugins\\Assets\\Palettes");
            SetValues();
        }

        private void Nud_ValueChanged(object sender, RoutedPropertyChangedEventArgs<decimal> e)
        {
            if (settings == null) return;
            if (sender == firstNote) settings.firstNote = (int)firstNote.Value;
            if (sender == lastNote) settings.lastNote = (int)lastNote.Value + 1;
            if (sender == pianoHeight) settings.pianoHeight = (double)pianoHeight.Value / 100;
            if (sender == noteDeltaScreenTime) settings.deltaTimeOnScreen = (int)noteDeltaScreenTime.Value;
        }

        private void NoteDeltaScreenTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (settings == null) return;
            settings.deltaTimeOnScreen = noteDeltaScreenTime.Value;
        }

        private void BlackNotesAbove_Checked(object sender, RoutedEventArgs e)
        {
            if (settings == null) return;
            settings.blackNotesAbove = (bool)blackNotesAbove.IsChecked;
        }

        private void SameWidth_Checked(object sender, RoutedEventArgs e)
        {
            if (settings == null) return;
            settings.sameWidthNotes = (bool)sameWidth.IsChecked;
            blackNotesAbove.IsEnabled = !settings.sameWidthNotes;
        }
    }
}
