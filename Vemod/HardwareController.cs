using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vemod
{
    public class HardwareController
    {
        public static IKeyboard getSystemKeyboard()
        {
            Type t = Type.GetType("Mono.Runtime");
            if (t != null)
            {
                // We are running with the Mono VM (hopefully Linux :P)
                return new linuxKeyboard();
            }
            else
            {
                return new windowsKeyboard();
            }
        }

        public static IMouse getSystemMouse()
        {
            Type t = Type.GetType("Mono.Runtime");
            if (t != null)
            {
                // We are running with the Mono VM (hopefully Linux :P)
                return new linuxMouse();
            }
            else
            {
                return new windowsMouse();
            }
        }
    }
}
