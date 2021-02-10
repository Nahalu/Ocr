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
        public async Task<List<OcrResult>> ReadAsync(List<byte[]> images)
        {
            var results = new List<OcrResult>();
            foreach (var image in images) {
                var executingAssemblyPath = Assembly.GetExecutingAssembly().Location; 
                var executingPath = Path.GetDirectoryName(executingAssemblyPath);
                using var engine = new TesseractEngine(Path.Combine(executingPath, @"tessdata"), "fra", EngineMode.Default); 
                using var pix = Pix.LoadFromMemory(image);
            var test = engine.Process(pix);
            var text = test.GetText();
            var confidence = test.GetMeanConfidence();
            var result = new OcrResult();
            result.Text = text;
            result.Confidence = confidence;
            results.Add(result);
            }
            return results;
        }
    }
}