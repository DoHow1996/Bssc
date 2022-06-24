using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI
{
    /// <summary>
    /// EI工厂创造器，用于创建各种EI曲线的生成工厂。
    /// </summary>
    public class EIFactoryProducer
    {
        public static EICurveFactory CreateFactory(EICurveType type) {
            switch (type) {
                case EICurveType.EICD: {
                        return new EICDCurveFactory();
                    }
                default: return null;
            }
        }
    }
}
