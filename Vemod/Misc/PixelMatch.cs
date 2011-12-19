using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vemod
{
    public class PixelMatch
    {
        public int x1, x2, y1, y2;
        public float percent;

        public PixelMatch(int x1, int y1, int x2, int y2, float percent)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.percent = percent;
        }
    }
}
