using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ProjectModelsV
{
    [Serializable]
    public class BridgeModelV
    {
        /// <summary>
        /// 单位工程唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 单位工程名称
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// 单位工程编码
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 单位工程类型 0 road 1 bridge 3 underpass
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 单位工程创建者
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 单位工程创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 单位工程修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 单位工程起始桩号
        /// </summary>
        public double? StartMark { get; set; }
        /// <summary>
        /// 单位工程终止桩号
        /// </summary>
        public double? EndMark { get; set; }

        public string MainRoadSourceModelVId { get; set; }
        public string AffRoadSourceModelVId { get; set; }

        //public RoadSourceModelV mainRoadSourceModelV;
        //public RoadSourceModelV affRoadSourceModelV;

        public List<SubSModelV> subSModelVs = new List<SubSModelV>();
        public List<SupportSModelV> supportSModelVs = new List<SupportSModelV>();
        public List<SuperSModelV> superSModelVs = new List<SuperSModelV>();

    }
}
