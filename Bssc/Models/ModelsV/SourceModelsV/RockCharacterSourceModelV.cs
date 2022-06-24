using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class RockCharacterSourceModelV
    {

        /// <summary>
        /// 岩层特性唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 单元id
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// 岩性索引名称
        /// </summary>
        public string RockLayerIndexName { get; set; }
        /// <summary>
        /// 岩性描述
        /// </summary>
        public string RockCharacterDesc { get; set; }
        /// <summary>
        /// 层顶标高 m
        /// </summary>
        public string TopLayereElevation { get; set; }
        /// <summary>
        /// 承载力基本
        /// 容许值（kPa）
        /// </summary>
        public string BasicAllowableValueOfBearingCapacity { get; set; }
        /// <summary>
        /// 饱和单轴抗压
        /// 强度标准值(kPa)
        /// </summary>
        public string SaturatedUniaxialCompressiveStrengthStandardValue { get; set; }
        /// <summary>
        /// 地基抗力系数（kN/m^3）
        /// </summary>
        public string GroundResistanceCoefficient { get; set; }
        /// <summary>
        /// 端阻发挥系数
        /// </summary>
        public string BottomResistanceOperateCoefficient { get; set; }
        /// <summary>
        /// 侧阻发挥系数
        /// </summary>
        public string LateralResistanceOperateCoefficient { get; set; }

    }
}
