using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//CAD开发库
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;

//CAD开发基础库
using BaseLibrary.Runtime;
using BaseLibrary.ResultData;
using BaseLibrary.ExtensionMethod;
using BaseLibrary.Filter;

namespace Bssc.Control.CadControl
{
    public class SnapEntityJigForDivideSpanLine :EntityJig
    {

        public Point3d m_Position = new Point3d();
        public Vector3d m_Direction;   //块方向
        private List<Curve> m_Curves = null;
        private double qszh;
        private double zzzh;

        private Line line;
        private Circle c = new Circle();
        public SnapEntityJigForDivideSpanLine(Entity entity, List<Curve> curves) : base(entity)
        {
            line = (Line)entity;
            m_Curves = curves;
            //((Circle)Entity).Center = m_CenterPt;
            //((Circle)Entity).Radius = m_Radius;


            //创建自定义对象捕捉模式
            CustomObjectSnapMode mode = new CustomObjectSnapMode("third", "_third", "三分之一", new BaseGlyph());
            //捕捉模式的实体类型为曲线
            mode.ApplyToEntityType(RXClass.GetClass(typeof(Curve)), CurveSnap);
            //开启自定义对象捕捉模式
            if (CustomObjectSnapMode.IsActive("_third"))
            {
                CustomObjectSnapMode.Deactivate("_third");
            }
            CustomObjectSnapMode.Activate("_third");
        }

        public void CurveSnap(ObjectSnapContext context, ObjectSnapInfo result)
        {
            // m_Curve = (Curve)context.PickedObject;//当前捕捉到的曲线对象

        }

        public void StopSnap()
        {
            //关闭自定义对象捕捉模式
            CustomObjectSnapMode.Deactivate("_third");
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions optJig = new JigPromptPointOptions();
            optJig.UserInputControls =
              (UserInputControls.Accept3dCoordinates |
              UserInputControls.NullResponseAccepted |
              UserInputControls.NoNegativeResponseAccepted);

            //optJig.UseBasePoint = true;
            //if (m_Curve != null)
            //{
            //    optJig.BasePoint = m_Curve.GetClosestPointTo(m_Position, false);
            //}


            // 用AcquirePoint函数得到用户输入的点.
            PromptPointResult resJigDis = prompts.AcquirePoint(optJig);
            Point3d curPt = new Point3d();


            curPt = resJigDis.Value; //获取满足输入选项的输入值




            if (resJigDis.Status == PromptStatus.Cancel)
            {
                return SamplerStatus.Cancel;
            }

            //if (m_Position != curPt)
            //{

            var orderCurves = (from r in m_Curves orderby (r.GetClosestPointTo(curPt, false)).DistanceTo(curPt) ascending select r).ToList<Curve>();



            m_Position = orderCurves[0].GetClosestPointTo(curPt, false);
            if (m_Position.DistanceTo(curPt) < 30)
            {
                if (orderCurves[0].Ex_IsCurvePoint(m_Position))
                {
                    m_Direction = orderCurves[0].Ex_GetNormalVector(m_Position);

                    //m_Direction = orderCurves[0].Ex_GetPerpendicularVector(m_Position); //获取垂直向量
                }
            }



            // 保存当前点.
            //m_Position = curPt;
            return SamplerStatus.NoChange;
            //}
            //else
            //{
            //    return SamplerStatus.NoChange;
            //}
        }

        protected override bool Update()
        {
            line.StartPoint = new Point3d(m_Position.X + 50 * m_Direction.X / m_Direction.Length, m_Position.Y + 50 * m_Direction.Y / m_Direction.Length, 0);
            line.EndPoint = new Point3d(m_Position.X - 50 * m_Direction.X / m_Direction.Length, m_Position.Y - 50 * m_Direction.Y / m_Direction.Length, 0);
            c.Center = m_Position;
            c.Radius = 5;

            return true;
        }

        public Entity getEntity()
        {
            return this.Entity;
        }


    }
}
