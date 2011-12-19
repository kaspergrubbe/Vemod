using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MouseKeyboardLibrary;
using System.Windows.Forms;

namespace Vemod
{
    public static class Keyboard
    {
        public static void write(int m)
        {
            switch (m)
            {
                case 0:
                    KeyboardSimulator.KeyPress(Keys.NumPad0);
                    break;
                case 1:
                    KeyboardSimulator.KeyPress(Keys.NumPad1);
                    break;
                case 2:
                    KeyboardSimulator.KeyPress(Keys.NumPad2);
                    break;
                case 3:
                    KeyboardSimulator.KeyPress(Keys.NumPad3);
                    break;
                case 4:
                    KeyboardSimulator.KeyPress(Keys.NumPad4);
                    break;
                case 5:
                    KeyboardSimulator.KeyPress(Keys.NumPad5);
                    break;
                case 6:
                    KeyboardSimulator.KeyPress(Keys.NumPad6);
                    break;
                case 7:
                    KeyboardSimulator.KeyPress(Keys.NumPad7);
                    break;
                case 8:
                    KeyboardSimulator.KeyPress(Keys.NumPad8);
                    break;
                case 9:
                    KeyboardSimulator.KeyPress(Keys.NumPad9);
                    break;
                default:
                    break;
            }
        }
    }
}
