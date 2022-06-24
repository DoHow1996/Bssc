using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class FoundationSourceModelV
    {

        /// <summary>
        /// 基础唯一编码
        /// </summary>
        [HeaderText("基础唯一编码")]
        [Visible(false)]
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [HeaderText("序号")]
        public int Serial { get; set; }
        /// <summary>
        /// 基础型号名
        /// </summary>
        [HeaderText("基础型号名")]
        public string Designation { get; set; }
        /// <summary>
        /// 基础类型
        /// </summary>
        [HeaderText("基础类型")]
        public string Type { get; set; }
        /// <summary>
        /// 承台厚度
        /// </summary>
        [HeaderText("承台厚度")]
        public double CapsThickness { get; set; }
        /// <summary>
        /// 桩径
        /// </summary>
        [HeaderText("桩径")]
        public double pileRadius { get; set; }
        /// <summary>
        /// 扩大基础入持力层最小值
        /// </summary>
        [HeaderText("扩大基础入持力层最小值")]
        public double ExpandTheMinimumValueOfTheFoundationIntoTheBearingLayer { get; set; }
        /// <summary>
        /// 承台截面   格式 2,2;-2,2;-2,-2;2,-2;
        /// </summary>
        [HeaderText("承台截面")]
        public string CapsSectionPoints { get; set; }
        /// <summary>
        /// 桩基布置   格式 2,2;-2,2;-2,-2;2,-2;
        /// </summary>
        [HeaderText("桩基布置")]
        public string PileCenterPoints { get; set; }

        public bool ischecked;
        public double capsXLen;
        public double capsYLen;
        public double pileXNum;
        public double pileYNum;
        public double pileXDis;
        public double pileYDis;

    }
}
