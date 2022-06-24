using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class CgxSourceModelV
    {
        /// <summary>
        /// 超高线唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 路线唯一编码
        /// </summary>
        public string CurveId { get; set; }
        /// <summary>
        /// 标志
        /// </summary>
        public int Symbol { get; set; }
        /// <summary>
        /// 高程设计线（即超高旋转轴）至左侧行车道外边缘线的距离（包括路缘带）
        /// </summary>
        public double Dtl { get; set; }
        /// <summary>
        /// 高程设计线（即超高旋转轴）至右侧行车道外边缘线的距离（包括路缘带）
        /// </summary>
        public double Dtr { get; set; }
        /// <summary>
        /// 土路肩横坡(％)
        /// </summary>
        public double Is1 { get; set; }
        /// <summary>
        /// 弯道内侧土路肩横坡和路面横坡的允许最大坡差(％)
        /// </summary>
        public double Ismax1 { get; set; }
        /// <summary>
        /// 弯道外侧土路肩横坡和路面横坡的允许最大坡差(％)
        /// </summary>
        public double Ismax2 { get; set; }
        /// <summary>
        /// 超高过渡方式标志
        /// </summary>
        public int Jd1 { get; set; }
        /// <summary>
        /// 超高旋转轴线至平面设计线的距离(米)
        /// </summary>
        public double Axi { get; set; }
        /// <summary>
        /// 超高旋转方式标志。
        /// </summary>
        public int Jd2 { get; set; }
        /// <summary>
        /// —桩号，自起点至终点，凡超高变化处均写一行
        /// </summary>
        public double St { get; set; }
        /// <summary>
        /// 路中线左侧横坡度
        /// </summary>
        public double Cgl { get; set; }
        /// <summary>
        /// 路中线右侧横坡度
        /// </summary>
        public double Cgr { get; set; }
        /// <summary>
        /// 有断链时，st的段落号，无断链时不能填写
        /// </summary>
        public double D { get; set; }
    }
}
