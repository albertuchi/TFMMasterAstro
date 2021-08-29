using System;

namespace NetCoreCode
{
    public class KPTModel
    {

        public int Line { get; set; }
        public int ModelNumber { get; set; }
        public double Age { get; set; }
        public double logL { get; set; }
        public double logTe { get; set; }
        public double HeCoreMass { get; set; }
        public double L3aLs { get; set; }
        public double ActualMass { get; set; }
        public double logTmax { get; set; }
        public double Metallicity { get; set; }

        public KPTModel(int line, 
                        int modelNumber, 
                        double logTYears, 
                        double logL,
                        double logTe, 
                        double HeCoreMass,
                        double l3aLs, 
                        double actualMass, 
                        double logTmax,
                        double metallicity)
        {
            this.Line = line;
            this.ModelNumber = modelNumber;
            this.Age = Math.Pow(10, logTYears) / Math.Pow(10, 6); // Changed the value from logt years to MYear
            this.logL = logL;
            this.logTe = logTe;
            this.HeCoreMass = HeCoreMass;
            this.L3aLs = l3aLs;
            this.ActualMass = actualMass;
            this.logTmax = logTmax;
            this.Metallicity = metallicity;
        }
    }
}