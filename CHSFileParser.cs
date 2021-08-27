using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetCoreCode
{
    public static class CHSFileParser
    {
        public static IEnumerable<CHSModel> ParseFile(string path)
        {
            var reader = File.OpenText(path);
            string line;
            var lineCount = 1;
            var models = new List<CHSModel>();
            while ((line = reader.ReadLine()?.Trim()) != null) 
            {
                var splittedLine = line.Split(' ').Where(value=> value != "").ToList();

                // we skip the first line
                if(lineCount > 2)
                {
                    var modelNumber = int.Parse(splittedLine.ElementAt(0));
                    var he4 = double.Parse(splittedLine.ElementAt(14));
                    var c12 = double.Parse(splittedLine.ElementAt(15));
                    var n14 = double.Parse(splittedLine.ElementAt(16));
                    var o16 = double.Parse(splittedLine.ElementAt(17));
                    var model = new CHSModel(modelNumber, he4, c12, n14, o16);
                    models.Add(model);
                }

                lineCount++;
            }
            NormalizeModelNumber(models);
            return models;            
        }

        public static void NormalizeModelNumber(IEnumerable<CHSModel> models)
        {
            var numberOfTimesModelNumberReset = 0; 
            for(var i=0; i< models.Count()-1; i++)
            {
                var currentModel = models.ElementAt(i);
                currentModel.NormalizedModelNumber = currentModel.ModelNumber + (10000 * numberOfTimesModelNumberReset);
                if (currentModel.ModelNumber > models.ElementAt(i+1).ModelNumber)
                {
                    numberOfTimesModelNumberReset++;
                }
            }
            var lastModel = models.ElementAt(models.Count() -1);
            lastModel.NormalizedModelNumber = lastModel.ModelNumber +  (10000 * numberOfTimesModelNumberReset);   
        }
    }
}
