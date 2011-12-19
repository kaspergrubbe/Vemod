using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Vemod
{
    public static class Screen
    {
        public static Color[,] takeScreenshot()
        {
            Bitmap screenShotBMP = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            Graphics screenShotGraphics = Graphics.FromImage(screenShotBMP);

            screenShotGraphics.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y, 0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size,
                CopyPixelOperation.SourceCopy);

            screenShotGraphics.Dispose();

            return Vemod.Image.bitmap2imagearray(screenShotBMP);
        }

        /*
         * Searches for a small image, in a big image and returns the matches
         * ---
         * Screen = image to search in
         * Sample = image to search for
         * Offset = where to START and END on the screen we're searching in
         * minMatch = minimum match in percent to even consider a beginning match (from 0.01 to 1) usually
         *            i use 0.70.
         */
        public static PixelMatchList SearchForSamples(Color[,] screen, Color[,] sample, Square offset, float minMatch)
        {
            PixelMatchList matches = new PixelMatchList();

            // search the screen for the sample
            int screen_rows = screen.GetLength(0);
            int screen_colums = screen.GetLength(1);

            // count sample rows
            int sample_rows = sample.GetLength(0);
            int sample_colums = sample.GetLength(1);

            // Det er her på Screenet vi starter vores søgning
            int i = offset.startx;
            int j = offset.starty;

            int k = 0;
            int l = 0;

            while (i < offset.endx)
            {
                while (j < offset.endy)
                {
                    // Vi har en start match, så herfra leder vi videre uden løkken kører videre
                    //if (screen[i, j].Equals(sample[k, l]))
                    if (Vemod.Image.rgbPercentCompare(screen[i, j], sample[k, l]) > minMatch)
                    {
                        // Pixelmatches:
                        int pixelmatches = 0;
                        float pixelmatchpercent = 0;
                        float pixelmatchpercenttotal = 0;

                        // Initial match!
                        int start_match_x = i;
                        int start_match_y = j;

                        // subsearch
                        // Vi vil ikke påvirke det omkringliggende loop
                        int _i = i;
                        int _j = j;

                        // Vi itererer over vores sample
                        while (k < sample_rows)
                        {
                            while (l < sample_colums)
                            {
                                // Tjek for om _i og _j er inden for vores "screen"
                                if (_i < offset.endx && _j < offset.endy)
                                {
                                    float match = Vemod.Image.rgbPercentCompare(screen[_i, _j], sample[k, l]);
                                    if (pixelmatchpercent == 0)
                                    {
                                        pixelmatchpercent = match;
                                        pixelmatchpercenttotal += match;
                                        pixelmatches++;
                                    }
                                    else
                                    {
                                        pixelmatchpercent = ((pixelmatchpercent * pixelmatches) + match) / (pixelmatches + 1);
                                        pixelmatchpercenttotal += match;
                                        pixelmatches++;
                                    }

                                    if (k == sample.GetLength(0) - 1 && l == sample.GetLength(1) - 1)
                                    {
                                        // Found the image!
                                        if (pixelmatchpercent > 0.70)
                                        {
                                            matches.Add(new PixelMatch(start_match_x, start_match_y, _i, _j, pixelmatchpercent));
                                        }

                                        k = sample_rows + 1; // Grimt grimt hack, men vi sparer en goto
                                        pixelmatchpercent = 0;
                                        pixelmatchpercenttotal = 0;
                                        pixelmatches = 0;
                                        break;
                                    }
                                    else if (l == sample.GetLength(1) - 1)
                                    {
                                        // Hvis vi er for "enden" så reset, og hop til næste row
                                        k++;
                                        l = 0;

                                        // Row jump på screenen også
                                        _i = _i + 1;
                                        _j = start_match_y;
                                    }
                                    else
                                    {
                                        // Hop en enkelt column både på screen og på sample
                                        l++;
                                        _j++;
                                    }
                                }
                                else
                                {
                                    k = sample_rows + 1; // Grimt grimt hack, men vi sparer en goto
                                    pixelmatchpercent = 0;
                                    pixelmatchpercenttotal = 0;
                                    pixelmatches = 0;
                                    break;
                                }
                            }
                        }
                        // resets the sample search
                        k = 0;
                        l = 0;
                    }
                    else
                    {
                        // Ingen match, så kan dette ikke være vores sample, led videre!
                        k = 0;
                        l = 0;
                    }
                    j++;
                }
                j = offset.starty;
                i++;
            }

            return matches;
        }
        public static PixelMatchList SearchForSamples(Color[,] screen, Color[,] sample)
        {
            return Vemod.Screen.SearchForSamples(screen, sample, new Vemod.Square(0,0,screen.GetLength(0),screen.GetLength(1)), (float)0.70);
        }
        public static PixelMatchList SearchForSamples(Color[,] screen, Color[,] sample, Square offset)
        {
            return Vemod.Screen.SearchForSamples(screen, sample, offset, (float)0.70);
        }
    }
}
