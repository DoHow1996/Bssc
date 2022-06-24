using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class RoadSourceModelV
    {
        public string Id { get; set; }
        public string Designation { get; set; }
        /// <summary>
        /// 横坡
        /// </summary>
        public double CrossSlope { get; set; }
        public string PqxFileName { get; set; }
        public string DmxFileName { get; set; }
        public string SqxFileName { get; set; }
        public string CgxFileName { get; set; }
        public List<DmxSourceModelV> dmxSourceModelVs { get; set; } = new List<DmxSourceModelV>();
        public List<PqxSourceModelV> pqxSourceModelVs { get; set; } = new List<PqxSourceModelV>();
        public List<SqxSourceModelV> sqxSourceModelVs { get; set; } = new List<SqxSourceModelV>();
        public List<CgxSourceModelV> cgxSourceModelVs { get; set; } = new List<CgxSourceModelV>();
    }
}
