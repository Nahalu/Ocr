using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Tesseract;

namespace Romain.Marino.Ocr
{
    public class Ocr
    {
        public async Task<IList<OcrResult>> ReadAsync(List<byte[]> images)
        {
            var results = new List<OcrResult>();
            var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var executingPath = Path.GetDirectoryName(executingAssemblyPath);
            var engine = new TesseractEngine(Path.Combine(executingPath, @"tessdata"), "fra", EngineMode.Default);

            foreach (byte[] image in images) { 
                var pix = Pix.LoadFromMemory(image);
            var test = engine.Process(pix);
            var Text = test.GetText();
            var Confidence = test.GetMeanConfidence();

            var result = new OcrResult();
            result.Text = Text;
            result.Confidence = Confidence;
            await Task.FromResult(result);
            results.Add(result);
            }

            return results;
        }
    }
}