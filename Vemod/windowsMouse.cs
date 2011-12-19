using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MouseKeyboardLibrary;

namespace Vemod
{
    class windowsMouse : IMouse
    {
        // Moves the mouse to a point
        public void move(Point p)
        {
            MouseSimulator.Position = new System.Drawing.Point(p.x, p.y);
        }

        // Moves the mouse to a set of coordinates
        public void move(int x, int y)
        {
            MouseSimulator.Position = new System.Drawing.Point(x, y);
        }

        // Makes a left click at the current location
        public void leftClick()
        {
            MouseSimulator.Click(MouseButton.Left);
        }

        // Moves the mouse, and then makes a leftclick at the location
        public void leftClick(Point p)
        {
            this.move(p);
            this.leftClick();
        }

        // Makes a right click at the current location
        public void rightClick()
        {
            MouseSimulator.Click(MouseButton.Right);
        }

        // Moves the mouse, and then makes a rightclick at the location
        public void rightClick(Point p)
        {
            this.move(p);
            this.rightClick();
        }

        // Returns the position of the mouse
        public Point getPosition()
        {
            return new Point(MouseSimulator.X, MouseSimulator.Y);
        }

        // Returns the X-coordinate of the current position of the mouse
        public int getPositionX()
        {
            return MouseSimulator.X;
        }

        // Returns the Y-coordinate of the current position of the mouse
        public int getPositionY()
        {
            return MouseSimulator.Y;
        }
    }
}
