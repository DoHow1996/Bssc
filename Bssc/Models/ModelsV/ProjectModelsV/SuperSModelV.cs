using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ProjectModelsV
{
    [Serializable]
    public class SuperSModelV
    {
        public string Id { get; set; }
        public string UnitId { get; set; }
        public string UniteNum { get; set; }
        public string StartPierNum { get; set; }
        public string EndPierNum { get; set; }
        public int SpanNum { get; set; }
        public string BeamId { get; set; }
        public string BeamType { get; set; }
        public string StartBeamHeight { get; set; }
        public string EndBeamHeight { get; set; }
    }
}
