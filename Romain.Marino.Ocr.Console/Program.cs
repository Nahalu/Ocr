using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Romain.Marino.Ocr.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var images = new List<byte[]>();
            foreach (var imagePath in args)
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                images.Add(imageBytes);
            }

            var ocrResults = await new Ocr().ReadAsync(images);
            foreach (var ocrResult in ocrResults)
            {
                System.Console.WriteLine($"Confidence : {ocrResult.Confidence}");
                System.Console.WriteLine($"Text : {ocrResult.Text}");
            }
        }
    }
}