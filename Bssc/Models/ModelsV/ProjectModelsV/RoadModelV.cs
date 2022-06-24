using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ProjectModelsV
{
    [Serializable]
    public class RoadModelV
    {
        /// <summary>
        /// 路线唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Serial { get; set; }

        /// <summary>
        /// 路线名称
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// 路线编号
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 路线唯一编码
        /// </summary>
        public string RoadSourceModelId { get; set; }

        //public RoadSourceModelV roadSourceModelV;
        public List<BridgeModelV> bridgeModelVs = new List<BridgeModelV>();

    }
}
