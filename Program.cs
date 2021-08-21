﻿using System;
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
            Exporter.Initialize();
            
            /*Test model with the instructions provided by Santi
            var kptModelstest = KPTFileParser.ParseFile("C:/Users/alber/OneDrive/Máster VIU/Asignaturas/14MAST Trabajo Fin de Máster/NetCoreCode/file_foragbext_ae\\FEHp006\\0090z332y290aede1.kpt");
            var kptModeltest= KPTModelFinder.FindModel(kptModelstest);*/

            var directories = Directory.GetDirectories(@"C:/Users/alber/OneDrive/Máster VIU/Asignaturas/14MAST Trabajo Fin de Máster/NetCoreCode/file_foragbext_ae");
            foreach(var directory in directories)
            {
                string[] kptFiles = Directory.GetFiles(directory, "*.kpt");
                string[] chsFiles = Directory.GetFiles(directory, "*.chs");

                foreach(var file in kptFiles)
                {
                    var kptModels = KPTFileParser.ParseFile(file);
                    var keyPointModel = kptModels.First(); // We start parsing from the last KeyPoint
                    if(keyPointModel.logTmax < 8.6 && kptModels.Count() > 1) // Is the 8.6 included???; some times kptmodels just have 1 element, we skip those
                    {
                        var kptModel = KPTModelFinder.FindModel(kptModels);
                        if(kptModel == null)
                        {
                            continue; // This means that we have not found any local minumin
                        }
                        var fileModelNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                        var modelName = Path.GetFileName(file);
                        var chsFile = chsFiles.Single(f=> f.Contains(fileModelNameWithoutExtension));
                        var chsModels = CHSFileParser.ParseFile(chsFile);
                        var chsModel = CHSModelFinder.FindModel(chsModels,kptModel);
                        if(chsModel != null) //Remove this after solving issue with missing chsmodel
                        {
                            var initialMass = double.Parse(modelName.Substring(0, 4)) * Math.Pow(10, -2);
                            Exporter.Export(kptModel, chsModel, modelName, initialMass);
                        }
                    }
                    else 
                    {
                        break;
                    }
                }
            }
                        
            Console.WriteLine("Completed!");
        }
    }
}
