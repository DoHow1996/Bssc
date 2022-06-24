using Bssc.Models.ModelsV.ProjectModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV
{
    public static class GlobalData
    {
        public static ProjectModelV projectModelV = new ProjectModelV();
        public static SourceModelV sourceModelV = new SourceModelV();
        public static List<SupportSourceModelV> supportSourceModelVs = new List<SupportSourceModelV>();
    }
}
