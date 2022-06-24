using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsSupport
    {
        /// <summary>
        /// 支座唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 联号
        /// </summary>
        public string ChainNum { get; set; }
        /// <summary>
        /// 墩号
        /// </summary>
        public string PierNum { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public int? Position { get; set; }
        /// <summary>
        /// 最大标准组合反力
        /// </summary>
        public double? MaximumStandardCombinationReaction { get; set; }
        /// <summary>
        /// 拟选支座承载力
        /// </summary>
        public double? ProposedSupportBearingCapacity { get; set; }
        /// <summary>
        /// 0否1是
        /// </summary>
        public int? FixSupport { get; set; }
        /// <summary>
        /// dx单向 sx双向 gd固定
        /// </summary>
        public string LimitDirection { get; set; }
        /// <summary>
        /// 支座与分跨线距离（mm）
        /// </summary>
        public double? DistanceBetweenSupportAndSpanline { get; set; }
        /// <summary>
        /// 支座与中心线距离（mm）
        /// </summary>
        public double? DistanceBetweenSupportAndCenterline { get; set; }
        /// <summary>
        /// 支反力恒载
        /// </summary>
        public double? SupportReactionDeadLoad { get; set; }
        /// <summary>
        /// 上垫石厚度（mm）
        /// </summary>
        public double? ThicknessOfUpperStone { get; set; }
        /// <summary>
        /// 纵向偏心（mm）
        /// </summary>
        public double? LongitudinalEccentricF { get; set; }
        /// <summary>
        /// 桥墩型号厚度（mm）
        /// </summary>
        public double? PierTypeThickness { get; set; }
        /// <summary>
        /// 桥墩计算厚度（mm）
        /// </summary>
        public double? PierCalculateThickness { get; set; }
        /// <summary>
        /// 名义支座系统高度（mm）
        /// </summary>
        public double? NominalSupportSystemH1 { get; set; }
        /// <summary>
        /// 实际支座系统高度（mm）
        /// </summary>
        public double? ActualSupportSystemH2 { get; set; }
        /// <summary>
        /// 支座上垫石尺寸a（mm）
        /// </summary>
        public double? UpperStoneSizeA { get; set; }
        /// <summary>
        /// 支座上垫石尺寸b（mm）
        /// </summary>
        public double? UpperStoneSizeB { get; set; }
        /// <summary>
        /// 上座板尺寸
        /// </summary>
        public double? UpperBoardSize { get; set; }
        /// <summary>
        /// 上垫石m
        /// </summary>
        public double? UpperStoneSizeM { get; set; }
        /// <summary>
        /// 上垫石n
        /// </summary>
        public double? UpperStoneSizeN { get; set; }
        /// <summary>
        /// 上垫石厚度（mm）
        /// </summary>
        public double? UpperStoneThickness { get; set; }
        /// <summary>
        /// 上垫石-1#钢筋长度（mm）
        /// </summary>
        public double? UpperStoneSteel1Length { get; set; }
        /// <summary>
        /// 上垫石-2#钢筋长度（mm）
        /// </summary>
        public double? UpperStoneSteel2Length { get; set; }
        /// <summary>
        /// 上垫石-3#钢筋长度（mm）
        /// </summary>
        public double? UpperStoneSteel3Length { get; set; }
        /// <summary>
        /// 上垫石-4#钢筋长度（mm）
        /// </summary>
        public double? UpperStoneSteel4Length { get; set; }
        /// <summary>
        /// 上垫石单重（kg/m）
        /// </summary>
        public double? UpperStoneSingleWeight { get; set; }
        /// <summary>
        /// 下垫石尺寸c（mm）
        /// </summary>
        public double? LowerStoneSizeC { get; set; }
        /// <summary>
        /// 下垫石尺寸d（mm）
        /// </summary>
        public double? LowerStoneSizeD { get; set; }
        /// <summary>
        /// 下垫石厚度（mm）
        /// </summary>
        public double? LowerStoneThickness { get; set; }
        /// <summary>
        /// 下垫石m
        /// </summary>
        public double? LowerStoneSizeM { get; set; }
        /// <summary>
        /// 下垫石n
        /// </summary>
        public double? LowerStoneSizeN { get; set; }
        /// <summary>
        /// 下垫石-1#钢筋长度（mm）
        /// </summary>
        public double? LowerStoneSteel1Length { get; set; }
        /// <summary>
        /// 下垫石-2#钢筋长度（mm）
        /// </summary>
        public double? LowerStoneSteel2Length { get; set; }
        /// <summary>
        /// 下垫石-3#钢筋长度（mm）
        /// </summary>
        public double? LowerStoneSteel3Length { get; set; }
        /// <summary>
        /// 下垫石-4#钢筋长度（mm）
        /// </summary>
        public double? LowerStoneSteel4Length { get; set; }
        /// <summary>
        /// 下垫石-单重（kg/m）
        /// </summary>
        public double? LowerStoneSingleWeight { get; set; }
        /// <summary>
        /// 垫石混凝土C50（m3）
        /// </summary>
        public double? StoneConcreteVolume { get; set; }
        /// <summary>
        /// 支座资源唯一编码
        /// </summary>
        public string SupportResId { get; set; }

        //public virtual ResSupport? SupportRes { get; set; }
    }
}
