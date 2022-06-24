using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ProjectModelsV
{
    [Serializable]
    public class SpanLineModelV
    {
        /// <summary>
        /// 分跨线编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 单元编码
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 桩号
        /// </summary>
        public double Mark { get; set; }
        /// <summary>
        /// 墩号
        /// </summary>
        public double PierNum { get; set; }
        /// <summary>
        /// 分跨线斜交角
        /// </summary>
        public double Angle { get; set; }
        /// <summary>
        /// 间距
        /// </summary>
        public double distance { get; set; }

    }
}
