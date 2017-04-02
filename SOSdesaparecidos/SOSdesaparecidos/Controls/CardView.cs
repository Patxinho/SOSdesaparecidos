using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SOSdesaparecidos.Controls
{
    public class CardView : Frame
    {
        public CardView()
        {
            Padding = 0;
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                HasShadow = false;
                OutlineColor = Color.Transparent;
                BackgroundColor = Color.Transparent;
            }
        }
    }
}
