using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ProjectModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Control.DataControl.SaveAndLoadData
{
    [Serializable]
    public class TempData
    {

        public SourceModelV sourceModelV { get; set; }
        public ProjectModelV projectModelV { get; set; }
        public List<SupportSourceModelV> supportSourceModelVs = new List<SupportSourceModelV>();
        public TempData()
        {

        }

    }
}
