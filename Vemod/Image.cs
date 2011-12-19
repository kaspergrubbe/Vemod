using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Vemod
{
    public static class Image
    {
        public static float rgbPercentCompare(Color c1, Color c2)
        {
            int r1 = c1.R;
            int g1 = c1.G;
            int b1 = c1.B;
            int r2 = c2.R;
            int g2 = c2.G;
            int b2 = c2.B;

            float r, g, b;

            if (r1 < r2)
                r = (float)(r1 + 1) / (r2 + 1);
            else
                r = (float)(r2 + 1) / (r1 + 1);

            if (g1 < g2)
                g = (float)(g1 + 1) / (g2 + 1);
            else
                g = (float)(g2 + 1) / (g1 + 1);

            if (b1 < b2)
                b = (float)(b1 + 1) / (b2 + 1);
            else
                b = (float)(b2 + 1) / (b1 + 1);


            float res = (r + g + b) / 3;
            return res;
        }

        public static Color[,] bitmap2imagearray(Bitmap b)
        {
            Color[,] imgArray = new Color[b.Width, b.Height];
            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    imgArray[x, y] = b.GetPixel(x, y);
                }
            }
            return imgArray;
        }

        public static Color[,] file2imagearray(String path)
        {
            Bitmap b = new Bitmap(Bitmap.FromFile(path));
            return Vemod.Image.bitmap2imagearray(b);
        }

        public static void imagearray2file(Color[,] image, String filename)
        {
            Bitmap b = new Bitmap(image.GetLength(0), image.GetLength(1));

            int width = image.GetLength(0);
            int height = image.GetLength(1);

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    b.SetPixel(x, y, image[x,y]);
                }
            }

            b.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        /*
         * Cuts off a image inside a bigger picture
         * ---
         * Screen = image to search in
         * Offset = where to START and END on the screen we're searching in
         */
        public static Color[,] subImage(Color[,] screen, Square offset)
        {
            // search the screen for the sample
            int screen_rows = screen.GetLength(0);
            int screen_colums = screen.GetLength(1);

            Color[,] subImage = new Color[offset.endx - offset.startx, offset.endy - offset.starty];

            // Det er her på vores image vi starter vores søgning
            int i = offset.startx; // x
            int j = offset.starty; // y

            // new image
            int ni = 0;
            int nj = 0;

            while (j < offset.endy)
            {
                while (i < offset.endx)
                {
                    // Vi itererer over vores sample
                    subImage[ni, nj] = screen[i, j];
                    Console.WriteLine("(" + ni + ";" + nj + " -> (" + i + ";" + j + ")");

                    ni++;
                    i++;
                }
                ni = 0;
                i = offset.startx;
                nj++;
                j++;
            }

            return subImage;
        }

        public static Color[,] applyFilter(Color[,] screen)
        {
            // search the screen for the sample
            int screen_rows = screen.GetLength(0);
            int screen_colums = screen.GetLength(1);

            int k = 0;
            int l = 0;

            Color[,] subImage = new Color[screen_rows, screen_colums];

            // Vi itererer over vores sample
            while (k < (screen_rows))
            {
                while (l < (screen_colums))
                {
                    // COPY!
                    /*int filter = int.Parse("" + screen[i, j].R + screen[i, j].G + screen[i, j].B);
                    if (filter <= 000)
                    {*/
                    Color c = screen[k, l];
                    if ((int)c.R > 200 || (int)c.G > 200 || (int)c.B > 200)
                    {
                    }
                    else
                    {
                        int grayScale = (int)((screen[k, l].R * .3) + (screen[k, l].G * .59) + (screen[k, l].B * .11));
                        Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                        subImage[k, l] = newColor;
                        //subImage[k, l] = screen[i, j];
                    }
                    /*}*/

                    if (k == screen_rows - 1 && l == screen_colums - 1)
                    {
                        k = screen_rows + 1; // Grimt grimt hack, men vi sparer en goto
                        break;
                    }
                    else if (l == screen_colums - 1)
                    {
                        // Hvis vi er for "enden" så reset, og hop til næste row
                        k++;
                        l = 0;

                        // Row jump på screenen også
                    }
                    else
                    {
                        // Hop en enkelt column både på screen og på sample
                        l++;
                    }
                }
            }


            return subImage;
        }

        static public Color[,] shrinkImage(Color[,] image)
        {
            // search the screen for the sample
            int image_rows = image.GetLength(0);
            int image_colums = image.GetLength(1);

            int rowsToRemoveLeft = 0;

            int k = 0;
            int l = 0;
            // Vi itererer over vores image
            while (k < image_rows)
            {
                bool canRemoveColumn = true;
                while (l < image_colums)
                {
                    Color c = image[k, l];
                    if ((int)c.A == 0 && (int)c.R == 0 && (int)c.G == 0 && (int)c.B == 0)
                    {
                    }
                    else
                    {
                        canRemoveColumn = false;
                        break;
                    }
                    l++;
                }

                if (canRemoveColumn)
                {
                    rowsToRemoveLeft++;
                }
                else
                {
                    break;
                }
                l = 0;
                k++;
            }

            int rowsToRemoveRight = 0;

            k = image_rows - 1;
            l = image_colums - 1;
            // Vi itererer over vores image
            while (k > 0)
            {
                bool canRemoveColumn = true;
                while (l > 0)
                {
                    Color c = image[k, l];
                    if ((int)c.A == 0 && (int)c.R == 0 && (int)c.G == 0 && (int)c.B == 0)
                    {
                    }
                    else
                    {
                        canRemoveColumn = false;
                        break;
                    }
                    l--;
                }

                if (canRemoveColumn)
                {
                    rowsToRemoveRight++;
                }
                else
                {
                    break;
                }
                l = image_colums - 1;
                k--;
            }

            int columsToRemoveTop = 0;

            k = 0;
            l = 0;
            // Vi itererer over vores image
            while (l < image_colums)
            {
                bool canRemoveRow = true;
                while (k < image_rows)
                {
                    Color c = image[k, l];
                    if ((int)c.A == 0 && (int)c.R == 0 && (int)c.G == 0 && (int)c.B == 0)
                    {
                    }
                    else
                    {
                        canRemoveRow = false;
                        break;
                    }
                    k++;
                }

                if (canRemoveRow)
                {
                    columsToRemoveTop++;
                }
                else
                {
                    break;
                }
                k = 0;
                l++;
            }

            int columsToRemoveBottom = 0;

            k = image_rows - 1;
            l = image_colums - 1;
            // Vi itererer over vores image
            while (l > 0)
            {
                bool canRemoveRow = true;
                while (k > 0)
                {
                    Color c = image[k, l];
                    if ((int)c.A == 0 && (int)c.R == 0 && (int)c.G == 0 && (int)c.B == 0)
                    {
                    }
                    else
                    {
                        canRemoveRow = false;
                        break;
                    }
                    k--;
                }

                if (canRemoveRow)
                {
                    columsToRemoveBottom++;
                }
                else
                {
                    break;
                }
                k = image_rows - 1;
                l--;
            }

            Square s = new Square(rowsToRemoveLeft, columsToRemoveTop, (image_rows - rowsToRemoveRight), (image_colums - columsToRemoveBottom));
            Color[,] returnImage = Image.subImage(image, s);

            Console.WriteLine("({0};{1}) - ({2};{3})", columsToRemoveTop, rowsToRemoveLeft, (image_rows - columsToRemoveBottom), (image_colums - rowsToRemoveRight));
            Console.WriteLine("Rows top: " + columsToRemoveTop);
            Console.WriteLine("Rows bottom: " + columsToRemoveBottom);
            Console.WriteLine("Colum left: " + rowsToRemoveLeft);
            Console.WriteLine("Colum right: " + rowsToRemoveRight);

            return returnImage;
        }
    }
}
