﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.Helpers
{
    public sealed class OnCustomPlatform<T>
    {
        public OnCustomPlatform()
        {
            Android = default(T);
            iOS = default(T);
            WinPhone = default(T);
            Windows = default(T);
            Other = default(T);
        }

        public T Android { get; set; }

        public T iOS { get; set; }

        public T WinPhone { get; set; }

        public T Windows { get; set; }

        public T Other { get; set; }

        public static implicit operator T(OnCustomPlatform<T> onPlatform)
        {
            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.Android:
                    return onPlatform.Android;
                case Xamarin.Forms.Device.iOS:
                    return onPlatform.iOS;
                case Xamarin.Forms.Device.WinPhone:
                    return onPlatform.WinPhone;
                case Xamarin.Forms.Device.Windows:
                    if (Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Desktop)
                        return onPlatform.Windows;
                    else
                        return onPlatform.WinPhone;
                default:
                    return onPlatform.Other;
            }
        }
    }
}
