using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace NetCoreCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            var filesWithProblem = new List<String>(); //List to control errors

            var directories = Directory.GetDirectories(@"C:/file_foragbext_ae"); //Parsed to the root path
            foreach (var directory in directories)
            {
                string[] kptFiles = Directory.GetFiles(directory, "*.kpt");
                string[] chsFiles = Directory.GetFiles(directory, "*.chs");
                var exportInformations = new List<ExportInformation>();

                foreach (var file in kptFiles)
                {
                    var kptModels = KPTFileParser.ParseFile(file);
                    var keyPointModel = kptModels.FirstOrDefault(); // We start parsing from the last KeyPoint
                    if (keyPointModel == null || kptModels.Count() <= 1) // Some times kptmodels just have 1 element, we skip those
                    {
                        filesWithProblem.Add($"File: {file}, Problem: The kptmodel list parsed is empty or just have one element");
                        continue;
                    }
                    if (keyPointModel.logTmax < 8.6)
                    {
                        var kptModel = KPTModelFinder.FindModel(kptModels);
                        if (kptModel == null)
                        {
                            filesWithProblem.Add($"File: {file}, Problem: The minimum could not be found (not change of tendency?)");
                            continue; // This means that we have not found any local minumin
                        }
                        var fileModelNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                        var modelName = Path.GetFileName(file);
                        var folder = Path.GetFileName(directory); 
                        var chsFile = chsFiles.Single(f => f.Contains(fileModelNameWithoutExtension));
                        var chsModels = CHSFileParser.ParseFile(chsFile);
                        var chsModel = CHSModelFinder.FindModel(chsModels, kptModel);
                        var initialMass = double.Parse(modelName.Substring(0, 4)) * Math.Pow(10, -2);
                        var fileName = $"Initialtab_{folder}.agb";
                        var exportInformation = new ExportInformation(kptModel, chsModel, modelName, initialMass, fileName);
                        exportInformations.Add(exportInformation);
                    }
                    else
                    {
                        break;
                    }
                }
                Exporter.Export(exportInformations);
            }

            foreach (var file in filesWithProblem)
            {
                Console.WriteLine(file);
            }

            Console.WriteLine("Completed!");
        }
    }
}
