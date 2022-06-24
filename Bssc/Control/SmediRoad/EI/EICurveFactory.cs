using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI
{
    /// <summary>
    /// EI曲线工厂，是所有工厂基类
    /// </summary>
    public abstract class EICurveFactory
    {
        
        /// <summary>
        /// 加载EI文件
        /// </summary>
        /// <param name="filename">文件路径及文件名</param>
        public abstract List<EICDUnit> Load(string filename);
        public abstract List<EICDUnit> LoadData(List<PqxSourceModelV> pqxSourceModelVs);
        /// <summary>
        /// 获取带凸度属性的几何点结果集
        /// </summary>
        /// <returns></returns>
        public abstract List<List<EICDPoint>> GetGeoEICurve();

        protected abstract void Build();
    }
}
