using System;

namespace NetCoreCode
{
    public class ExportInformation
    {
        public KPTModel KPTModel {get; set;} 
        public CHSModel CHSModel {get; set;} 
        public string ModelName {get; set;} 
        public double InitialMass {get; set;} 
        public string ExportFileName {get; set;}

        public ExportInformation(KPTModel kPTModel, CHSModel cHSModel, string modelName, double initialMass, string exportFileName)
        {
            this.KPTModel = kPTModel;
            this.CHSModel = cHSModel;
            this.ModelName = modelName;
            this.InitialMass = initialMass;
            this.ExportFileName = exportFileName;
        }
    }
}