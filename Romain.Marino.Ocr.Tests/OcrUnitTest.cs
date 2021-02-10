using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Romain.Marino.Ocr.Tests
{
    public class OcrUnitTest
    {
        [Fact]
        public async Task ImagesShouldBeReadCorrectly()
        {
            var executingPath = GetExecutingPath();
            var images = new List<byte[]>();
            foreach (var imagePath in
                Directory.EnumerateFiles(Path.Combine(executingPath, "Images")))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                images.Add(imageBytes);
            }

            var ocrResults = await new Ocr().ReadAsync(images);

            Assert.Equal(ocrResults[0].Text, @"plusieurs examens pour être validées.\n Avec l'évolution des outils et des\n tehnologies, il faut aussi repasser les\n");
            Assert.Equal(ocrResults[0].Confidence, 1);
            Assert.Equal(ocrResults[1].Text, @"examens de temps en temps.\n La réelle reconnaissance vient quand un\n développeur devient MVP. Cela indique\n");
            Assert.Equal(ocrResults[1].Confidence, 1);
            Assert.Equal(ocrResults[2].Text, @"développeur devient MVP. Cela indique\n une implication dans la communauté et\n une connaissance de son domaine (C#,\n");
            Assert.Equal(ocrResults[2].Confidence, 1);
        }
        private static string GetExecutingPath()
        {
            var executingAssemblyPath =
                Assembly.GetExecutingAssembly().Location;
            var executingPath =
                Path.GetDirectoryName(executingAssemblyPath);
            return executingPath;
        }
    } 
}