using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ProjectModelsV
{
    [Serializable]
    public class SupportSModelV
    {

        public string Id { get; set; }
        public int index { get; set; }
        public string UnitId { get; set; }
        public string ChainNum { get; set; }
        public string PierNum { get; set; }
        public int Position { get; set; }
        public double MaximumStandardCombinationReaction { get; set; }
        public double MinimumStandardCombinationReaction { get; set; }
        public double ProposedSupportBearingCapacity { get; set; }
        public int FixSupport { get; set; }
        public string LimitDirection { get; set; }
        public string SupportType { get; set; }
        public double SupportSystemPredefineHeight { get; set; }
        public double DistanceBetweenSupportAndSpanline { get; set; }
        public double DistanceBetweenSupportAndCenterline { get; set; }
        
        /// <summary>
        /// 支反力恒载
        /// </summary>
        public double SupportReactionDeadLoad { get; set; }
        /// <summary>
        /// 上垫石厚度（mm）
        /// </summary>
        public double ThicknessOfUpperStone { get; set; }
        /// <summary>
        /// 纵向偏心（mm）
        /// </summary>
        public double LongitudinalEccentricF { get; set; }
        /// <summary>
        /// 桥墩型号厚度（mm）
        /// </summary>
        public double PierTypeThickness { get; set; }
        /// <summary>
        /// 桥墩计算厚度（mm）
        /// </summary>
        public double PierCalculateThickness { get; set; }
        /// <summary>
        /// 上垫石最小包络长
        /// </summary>
        public double MinimumEnvelopeLengthOfUpperStone { get; set; }
        /// <summary>
        /// 上垫石最小包络宽
        /// </summary>
        public double MinimumEnvelopeWidthOfUpperStone { get; set; }
    }
}
