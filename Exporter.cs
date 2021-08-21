using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetCoreCode
{
    public static class Exporter
    {
        private const string ExportFileName = "initialtab.agb";
        public static void Initialize()
        {
            if (!File.Exists(ExportFileName))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(ExportFileName))
                {
                    sw.WriteLine(" XX  Z= .XXXX  1  (alpha enhanced)           ");
                    sw.WriteLine("===========================================================================================================================");
                    sw.WriteLine("   INPUT FILE           M(in)/Mo  AGE(My)  log(L)  log(Te)   M/Mo      Mc_He   He_s        C_s         N_s         O_s");
                    sw.WriteLine("===========================================================================================================================");
                }	
            }

        }

        public static void Export(KPTModel kPTModel, 
                                  CHSModel cHSModel, 
                                  string fileName, 
                                  double initialMass)
        {
            using (StreamWriter sw = File.AppendText(ExportFileName))
            {
                var formattedLine = string.Format($"{fileName}     {initialMass.ToString("F3")}  {kPTModel.Age.ToString("00000.00")}  {kPTModel.logL.ToString("F4")}  {kPTModel.logTe.ToString("F4")}  {kPTModel.ActualMass.ToString("F6")}  {kPTModel.HeCoreMass.ToString("F4")}  {cHSModel.He4.ToString("0.0000E+00")}  {cHSModel.C12.ToString("0.0000E+00")}  {cHSModel.N14.ToString("0.0000E+00")}  {cHSModel.O16.ToString("0.0000E+00")}");
                sw.WriteLine(formattedLine);
            }	
        }
    }
}