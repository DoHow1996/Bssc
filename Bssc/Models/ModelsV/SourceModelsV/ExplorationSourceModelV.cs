using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class ExplorationSourceModelV
    {

        /// <summary>
        /// 地勘孔唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 每一个地勘孔的id
        /// </summary>
        public string UnitId { get; set;}
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 坐标X
        /// </summary>
        public string PointX { get; set; }
        /// <summary>
        /// 坐标Y
        /// </summary>
        public string PointY { get; set; }
        /// <summary>
        /// 高程
        /// </summary>
        public string Elevation { get; set; }
        /// <summary>
        /// 孔深
        /// </summary>
        public string HoleDepth { get; set; }
        /// <summary>
        /// 地下水稳定水位深度
        /// </summary>
        public string GroundwaterStableWaterLevelDepth { get; set; }
        /// <summary>
        /// 地下水稳定水位高程
        /// </summary>
        public string GroundwaterStableWaterLevelElevation { get; set; }
        /// <summary>
        /// 土层特征 界面需要的数据
        /// </summary>
        public string SoilCharacter { get; set; }

    }
}
