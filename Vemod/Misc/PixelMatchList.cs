using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Vemod
{
    public class PixelMatchList : CollectionBase
    {
        private List<PixelMatch> pixelmatch = new List<PixelMatch>();
        private PixelMatch bestMatch;
        public float maxpercent = 0;


        public int Count   // the Name property
        {
            get
            {
                return this.pixelmatch.Count;
            }
        }

        public PixelMatchList()
        {

        }

        public PixelMatch this[int index]
        {

            get { return pixelmatch[index]; }

            set
            {
                pixelmatch[index] = value;
            }

        }

        public void Add(PixelMatch px)
        {
            if (px.percent > this.maxpercent)
            {
                this.maxpercent = px.percent;
                this.bestMatch = px;
            }
            this.pixelmatch.Add(px);
        }

        public PixelMatch getBestMatch()
        {
            return this.bestMatch;
        }

    }
}
