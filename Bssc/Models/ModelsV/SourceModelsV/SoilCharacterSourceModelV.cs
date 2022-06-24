using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class SoilCharacterSourceModelV
    {

        /// <summary>
        /// 土特征唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 单元Id
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// 土层索引名称
        /// </summary>
        public string SoilLayerIndexName { get; set; }
        /// <summary>
        /// 土性描述
        /// </summary>
        public string SoilCharacterDesc { get; set; }
        /// <summary>
        /// 层顶标高（m）
        /// </summary>
        public string TopLayerElevation { get; set; }
        /// <summary>
        /// 土的天然重度
        /// （kN / m ^ 3）
        /// </summary>
        public string SoilNaturalWeight { get; set; }
        /// <summary>
        /// 土的饱和重度
        /// （kN / m ^ 3）
        /// </summary>
        public string SoilSaturationWeight { get; set; }
        /// <summary>
        /// 是否透水
        /// </summary>
        public string IsPermeable { get; set; }
        /// <summary>
        /// 压缩模量（kPa）
        /// </summary>
        public string CompressionModulus { get; set; }
        /// <summary>
        /// 侧向抗力弹性比例系数
        /// （kN / m ^ 4）
        /// </summary>
        public string LateraResistanceElasticProportionalityCoefficient { get; set; }
        /// <summary>
        /// &quot;竖向抗力弹性比例系数
        /// （kN / m ^ 4）&quot;
        /// 
        /// </summary>
        public string VerticalResistanceElasticProportionalityCoefficient { get; set; }
        /// <summary>
        /// 土内摩擦角（度）
        /// 
        /// </summary>
        public string FrictionAngle { get; set; }
        /// <summary>
        /// &quot;土层与桩侧的摩擦力标
        /// 准值（kPa）&quot;
        /// 
        /// </summary>
        public string StandardValueOfFrictionBetweenSoilLayerAndPileSide { get; set; }
        /// <summary>
        /// 承载力基本容许值（kPa）
        /// &quot;是否为软土层
        /// 1：是 / 0：否&quot;
        /// 
        /// </summary>
        public string BasicAllowableValueOfBearingCapacity { get; set; }
        /// <summary>
        /// 是否软土
        /// </summary>
        public string IsSoftSoil { get; set; }
        /// <summary>
        /// 基底宽度修正系数
        /// </summary>
        public string BaseWidthCorrectionFactor { get; set; }
        /// <summary>
        /// 基底深度修正系数
        /// 
        /// </summary>
        public string BaseDepthCorrectionFactor { get; set; }
        /// <summary>
        /// &quot;桩端处图的承载力
        /// 容许值上限（kPa）&quot;
        /// 
        /// </summary>
        public string UpperLimitOfAllowableBearingCapacityInPileTipDiagram { get; set; }

    }
}
