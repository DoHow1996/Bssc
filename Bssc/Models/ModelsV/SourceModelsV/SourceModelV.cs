using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class SourceModelV
    {

        public List<BeamSourceModelV> beamSourceModelVs { get; set; } = new List<BeamSourceModelV>();
        public List<FoundationSourceModelV> foundationSourceModelVs { get; set; } = new List<FoundationSourceModelV>();
        public List<PierSourceModelV> pierSourceModelVs { get; set; } = new List<PierSourceModelV>();
        public List<RoadSourceModelV> roadSourceModelVs { get; set; } = new List<RoadSourceModelV>();
        public ExplorSourceModelV explorSourceModelV { get; set; } = new ExplorSourceModelV();

    }
}
