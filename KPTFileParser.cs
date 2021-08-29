using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetCoreCode
{
    public static class KPTFileParser
    {
        public static IEnumerable<KPTModel> ParseFile(string path)
        {
            var reader = File.OpenText(path);
            string line;
            var lineCount = 1;
            var keyPointCount =0;
            var lastKeyPointLine =0;
            var models = new List<KPTModel>();
            double metallicity = 0;
            
            while ((line = reader.ReadLine()?.Trim()) != null) 
            {
                if(lineCount == 1)
                {
                    keyPointCount = int.Parse(line);
                }

                var splittedLine = line.Split(' ').Where(value=> value != "").ToList();
                if(lineCount == (keyPointCount + 1))
                {
                    lastKeyPointLine = int.Parse(splittedLine.ElementAt(0));
                }
                if(lineCount == (keyPointCount + 2))
                {
                    metallicity = double.Parse(splittedLine[14]);
                }

                // we skip the lines before the lastKeyPoint line (including the offset of the KeyPointsCount, the first line and the three right above the models)
                if(lineCount >= (lastKeyPointLine + keyPointCount + 4))
                {
                    var modelNumber = int.Parse(splittedLine.ElementAt(0));
                    var logTYears = double.Parse(splittedLine.ElementAt(1));
                    var logL = double.Parse(splittedLine.ElementAt(3));
                    var logTe = double.Parse(splittedLine.ElementAt(4));
                    var HeCoreMass = double.Parse(splittedLine.ElementAt(8));
                    var l3aLs = double.Parse(splittedLine.ElementAt(13));
                    var actualMass = double.Parse(splittedLine.ElementAt(18));
                    var logTmax = double.Parse(splittedLine.ElementAt(19));
                    var model = new KPTModel(lineCount, modelNumber, logTYears, logL, logTe,HeCoreMass, l3aLs, actualMass, logTmax, metallicity);
                    models.Add(model);
                }

                lineCount++;
            }
            return models;            
        }
    }
}
