using System;

namespace NetCoreCode
{
    public class CHSModel
    {
        public int ModelNumber { get; set; }
        public int NormalizedModelNumber { get; set; }
        public double He4 { get; set; }
        public double C12 { get; set; }
        public double N14 { get; set; }
        public double O16 { get; set; }

        public CHSModel(int modelNumber, double he4, double c12, double n14, double o16)
        {
            this.ModelNumber = modelNumber;
            this.He4 = he4;
            this.C12 = c12;
            this.N14 = n14;
            this.O16 = o16;
        }
    }
}