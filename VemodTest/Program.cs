using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using Vemod;

namespace VemodTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing search times in picture.png...!");

            Program.testImage("picture.png", "mousey.png");
            Program.testImage("picture.png", "bar.png");
            Program.testImage("picture.png", "arrow.png");

            Console.WriteLine("Testing search times in bigpic.png...!");

            Program.testImage("bigpic.png", "chart.png");
            Program.testImage("bigpic.png", "checkbox.png");
            Program.testImage("bigpic.png", "downloads.png");

            Console.WriteLine("Test saving bigpic.png to bigpic2.png!");

            Color[,] cc = Vemod.Image.file2imagearray("bigpic.png");
            Vemod.Image.imagearray2file(cc, "bigpic2.png");

            Console.WriteLine("-- Test complete!");

            Console.ReadLine();
        }
        static void testImage(String bigImageName, String smallImageName)
        {
            Color[,] bigImage = Vemod.Image.file2imagearray(bigImageName);

            Console.WriteLine(smallImageName);
            Color[,] testImage = Vemod.Image.file2imagearray(smallImageName);
            DateTime startTime = DateTime.Now;
            PixelMatchList pml = Vemod.Screen.SearchForSamples(bigImage, testImage);
            TimeSpan duration = DateTime.Now.Subtract(startTime);
            Console.WriteLine("Found: " + pml.Count + " in " + duration.Seconds + "." + duration.Milliseconds + " seconds");
            Console.WriteLine("--");
        }
    }
}