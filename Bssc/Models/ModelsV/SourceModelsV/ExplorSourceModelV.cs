using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class ExplorSourceModelV
    {
        public string Id { get; set; }
        public string Designation { get; set; }
        public List<ExplorationSourceModelV> explorationSourceModelVs { get; set; } = new List<ExplorationSourceModelV>();
        public List<SoiLayerSourceModelV> soiLayerSourceModelVs { get; set; } = new List<SoiLayerSourceModelV>();
        public List<SoilCharacterSourceModelV> soilCharacterSourceModelVs { get; set; } = new List<SoilCharacterSourceModelV>();
        public List<RockCharacterSourceModelV> rockCharacterSourceModelVs { get; set; } = new List<RockCharacterSourceModelV>();
    }
}
