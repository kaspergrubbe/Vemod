using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MouseKeyboardLibrary;

namespace Vemod
{
    public static class Mouse
    {
        // Moves the mouse to a point
        public static void move(Point p)
        {
            MouseSimulator.Position = new System.Drawing.Point(p.x, p.y);
        }

        // Moves the mouse to a set of coordinates
        public static void move(int x, int y)
        {
            MouseSimulator.Position = new System.Drawing.Point(x, y);
        }

        // Makes a left click at the current location
        public static void leftClick()
        {
            MouseSimulator.Click(MouseButton.Left);
        }

        // Moves the mouse, and then makes a leftclick at the location
        public static void leftClick(Point p)
        {
            Mouse.move(p);
            Mouse.leftClick();
        }

        // Makes a right click at the current location
        public static void rightClick()
        {
            MouseSimulator.Click(MouseButton.Right);
        }

        // Moves the mouse, and then makes a rightclick at the location
        public static void rightClick(Point p)
        {
            Mouse.move(p);
            Mouse.rightClick();
        }

        // Returns the position of the mouse
        public static Point getPosition()
        {
            return new Point(MouseSimulator.X, MouseSimulator.Y);
        }

        // Returns the X-coordinate of the current position of the mouse
        public static int getPositionX()
        {
            return MouseSimulator.X;
        }

        // Returns the Y-coordinate of the current position of the mouse
        public static int getPositionY()
        {
            return MouseSimulator.Y;
        }
    }
}
