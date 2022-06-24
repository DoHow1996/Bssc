using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class SoiLayerSourceModelV
    {

        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Serial { get; set; }
        /// <summary>
        /// 每个单元的id
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 层号
        /// </summary>
        public string SoilLayerNum { get; set; }
        /// <summary>
        /// 层名
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// 钻孔编号
        /// </summary>
        public string HoleNum { get; set; }
        /// <summary>
        /// 地层特征唯一编码
        /// </summary>
        public string UnitExplorationUnitId { get; set; }
        
        /// <summary>
        /// 层顶埋深
        /// </summary>
        public string TopLayerDepth { get; set; }
        /// <summary>
        /// 层顶高程
        /// </summary>
        public string TopLayerElevation { get; set; }
        /// <summary>
        /// 层底埋深
        /// </summary>
        public string BottomLayerDepth { get; set; }
        /// <summary>
        /// 层底高程
        /// </summary>
        public string BottomLayerElevation { get; set; }
        /// <summary>
        /// 层厚
        /// </summary>
        public string LayerThickness { get; set; }
        /// <summary>
        /// 取芯率
        /// </summary>
        public string CoringRate { get; set; }
        /// <summary>
        /// RQD
        /// </summary>
        public string Rqd { get; set; }

    }

    public class SoilLayerCompare : IEqualityComparer<SoiLayerSourceModelV>
    {
        public bool Equals(SoiLayerSourceModelV x, SoiLayerSourceModelV y)
        {
            return x.SoilLayerNum == y.SoilLayerNum;
        }

        public int GetHashCode(SoiLayerSourceModelV obj)
        {
            return obj.SoilLayerNum.GetHashCode();
        }
    }
}
