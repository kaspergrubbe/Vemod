using System;
namespace Vemod
{
    public interface IMouse
    {
        Point getPosition();
        int getPositionX();
        int getPositionY();
        void leftClick();
        void leftClick(Point p);
        void move(int x, int y);
        void move(Point p);
        void rightClick();
        void rightClick(Point p);
    }
}
