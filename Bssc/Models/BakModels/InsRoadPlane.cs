using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsRoadPlane
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 路线唯一编码
        /// </summary>
        public string RoadId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 线元唯一编码
        /// </summary>
        public string UnitCurveId { get; set; }
        /// <summary>
        /// 桩号
        /// </summary>
        public double? Mark { get; set; }
        /// <summary>
        /// 偏心
        /// </summary>
        public double? EccentricityDistance { get; set; }
        /// <summary>
        /// 边线类别
        /// 0-绿化带边线；1-机动车道边线；2-非机动车道边线；3-人行道边线
        /// </summary>
        public int? Type { get; set; }
    }
}
