using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetCoreCode
{
    public static class Exporter
    {
        private static string filePath;
        public static void Initialize(List<ExportInformation> exportInformations)
        {
            var exportInfo = exportInformations.First();
            filePath = $"output/{exportInfo.ExportFileName}";
            var numberOfModels = exportInformations.Count();
            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine($" {numberOfModels}  Z= {exportInfo.KPTModel.Metallicity}  1  (alpha enhanced)           ");
                    sw.WriteLine("===========================================================================================================================");
                    sw.WriteLine("   INPUT FILE           M(in)/Mo  AGE(My)  log(L)  log(Te)   M/Mo      Mc_He   He_s        C_s         N_s         O_s");
                    sw.WriteLine("===========================================================================================================================");
                }	
            }

        }


        internal static void Export(List<ExportInformation> exportInformations)
        {
            Initialize(exportInformations);
            using (StreamWriter sw = File.AppendText(filePath))
            {
                foreach(var exportInfo in exportInformations)
                {
                    var kPTModel = exportInfo.KPTModel;
                    var cHSModel = exportInfo.CHSModel;
                    var formattedLine = string.Format($"{exportInfo.ModelName}     {exportInfo.InitialMass.ToString("F3")}  {kPTModel.Age.ToString("00000.00")}  {kPTModel.logL.ToString("F4")}  {kPTModel.logTe.ToString("F4")}  {kPTModel.ActualMass.ToString("F6")}  {kPTModel.HeCoreMass.ToString("F4")}  {cHSModel.He4.ToString("0.0000E+00")}  {cHSModel.C12.ToString("0.0000E+00")}  {cHSModel.N14.ToString("0.0000E+00")}  {cHSModel.O16.ToString("0.0000E+00")}");
                    sw.WriteLine(formattedLine);
                }	
            }  
            IncludeOutputSection(exportInformations);
        }

        internal static void IncludeOutputSection(List<ExportInformation> exportInformations)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine("===========================================================================================================================");
                sw.WriteLine("   OUTPUT FILE   ");
                sw.WriteLine("===========================================================================================================================");
                foreach(var exportInfo in exportInformations)
                {
                    var modelWithoutExtension = exportInfo.ModelName.Split('.')[0];
                    sw.WriteLine(modelWithoutExtension);
                }	
            }	
        }
    }
}