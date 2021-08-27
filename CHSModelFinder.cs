using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreCode
{
    public static class CHSModelFinder
    {
        public static CHSModel FindModel(IEnumerable<CHSModel> models, KPTModel kptModel)
        {
            var model = models.SingleOrDefault(m=> m.NormalizedModelNumber == kptModel.ModelNumber);

            if (model == null)
            {
                model = models.Last();
            }

            return model;
        }
    }
}