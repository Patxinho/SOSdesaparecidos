﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SOSdesaparecidos.Controls
{
    public partial class LabelSection : ContentView
    {
        public LabelSection()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(LabelSection), string.Empty);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TextProperty.PropertyName)
            {
                Section.Text = Device.RuntimePlatform == Device.iOS ? Text.ToUpperInvariant() : Text;
            }
        }
    }
}
