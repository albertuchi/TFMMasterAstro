using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreCode
{
    public static class KPTModelFinder
    {
        public static KPTModel FindModel(IEnumerable<KPTModel> models)
        {
            KPTModel minimumL3s = null; 
            var modelsToCheckTendency = new List<KPTModel>();
            for(var i=0; i< models.Count()-1; i++)
            {
                var isLocalMinimun = models.ElementAt(i).L3aLs < models.ElementAt(i+1).L3aLs; //This can fail, out of range exception
                if(isLocalMinimun) 
                {
                    modelsToCheckTendency = models.Skip(i+1).Take(100).ToList();
                    var localMinimunIsCorrect = modelsToCheckTendency.Min(m=> m.L3aLs) > models.ElementAt(i).L3aLs;
                    if(localMinimunIsCorrect)
                    {
                        minimumL3s = models.ElementAt(i);
                        break;
                    }   
                }

            }
            if(minimumL3s == null)
            {
                return null; // There are some cases where there are no more models after the last keypoint, so the serializer just return that value
            }
            var logLModelsToSearch = models.Where(m=> 
                                            m.Line >= minimumL3s.Line && 
                                            m.Line <= (minimumL3s.Line + 50)); 
            
            var maxlogL = logLModelsToSearch.Max(m=> m.logL);
            var model = logLModelsToSearch.First(m=> m.logL == maxlogL);
            return model;
        }
    }
}